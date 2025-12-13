using AutoMapper;
using AutoMapper.QueryableExtensions;
using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Results;
using DVLD_Application.Services.Interfaces.Humans.User;
using DVLD_Application.Services.Interfaces.Licenses.International;
using DVLD_Domain.Enums;
using DVLD_E_Enfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_E_Enfrastructure.Service.Implementaions.License.International
{
    public class InternationalLicenseService : IInternationalLicenseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public InternationalLicenseService(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Result<List<InternationalLicenseDto>>> GetAllInternationalLicensesAsync()
        {
            var dtos = await _context.InternationalLicenses
                .AsNoTracking()
                .ProjectTo<InternationalLicenseDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Result<List<InternationalLicenseDto>>.Success(dtos);
        }

        public async Task<Result<InternationalLicenseDto>> GetInternationalLicenseByIdAsync(int internationalLicenseId)
        {
            var license = await _context.InternationalLicenses
                .Include(l => l.Driver)
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == internationalLicenseId);

            if (license == null) return Result<InternationalLicenseDto>.Failure("International License not found.");

            var dto = _mapper.Map<InternationalLicenseDto>(license);
            return Result<InternationalLicenseDto>.Success(dto);
        }

        public async Task<Result<List<InternationalLicenseDto>>> GetInternationalLicensesByDriverIdAsync(int driverId)
        {
            var dtos = await _context.InternationalLicenses
                .Where(l => l.DriverID == driverId)
                .OrderByDescending(l => l.IssueDate)
                .AsNoTracking()
                .ProjectTo<InternationalLicenseDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Result<List<InternationalLicenseDto>>.Success(dtos);
        }

        public async Task<Result<bool>> HasActiveInternationalLicenseAsync(int driverId)
        {
            var hasActive = await _context.InternationalLicenses
                .AnyAsync(l => l.DriverID == driverId && l.IsActive && l.ExpirationDate > DateTime.UtcNow);
            return Result<bool>.Success(hasActive);
        }

        public async Task<Result<int>> IssueInternationalLicenseAsync(CreateNewInternationalLicenseDto dto)
        {
            var currentUserId = _currentUserService.GetCurrentUserId();
            if (!currentUserId.HasValue) return Result<int>.Failure("Current user not found.");



            var localLicense = await _context.Licenses.FindAsync(dto.IssuedUsingLocalLicenseID);
            if (localLicense == null || !localLicense.IsActive || localLicense.ExpirationDate < DateTime.UtcNow)
            {
                return Result<int>.Failure("Local license is invalid or expired.");
            }

            var expirationDate = DateTime.UtcNow.AddYears(1);
            if (expirationDate > localLicense.ExpirationDate)
            {
                expirationDate = localLicense.ExpirationDate;
            }

            try
            {
                var license = new DVLD_Domain.Models.InternationalLicense(
                    dto.ApplicationID,
                    dto.DriverID,
                    dto.IssuedUsingLocalLicenseID,
                    expirationDate,
                    currentUserId.Value
                );

                await _context.InternationalLicenses.AddAsync(license);
                

                var application = await _context.Applications.FindAsync(dto.ApplicationID);
                if (application != null)
                {
                    application.UpdateStatus(ApplicationStatus.Completed, currentUserId.Value);
                }

                await _context.SaveChangesAsync();
                return Result<int>.Success(license.Id);
            }
            catch (Exception ex)
            {
                return Result<int>.Failure(ex.Message);
            }
        }
    }
}
