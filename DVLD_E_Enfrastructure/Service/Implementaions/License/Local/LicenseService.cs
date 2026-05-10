using AutoMapper;
using AutoMapper.QueryableExtensions;
using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Results;
using DVLD_Application.Services.Interfaces.Humans.User;
using DVLD_Application.Services.Interfaces.Licenses.Local;
using DVLD_Domain.Enums;
using DVLD_E_Enfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_E_Enfrastructure.Service.Implementaions.License.Local
{
    public class LicenseService : ILicenseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public LicenseService(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Result<bool>> ActivateLicenseAsync(int licenseId)
        {
            var license = await _context.Licenses.FindAsync(licenseId);
            if (license == null) return Result<bool>.Failure("License not found.");

            var currentUserId = _currentUserService.GetCurrentUserId();
            if (!currentUserId.HasValue) return Result<bool>.Failure("Current user not found.");

            try
            {
                license.Activate(currentUserId.Value);
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }

        public async Task<Result<bool>> DeactivateLicenseAsync(int licenseId)
        {
            var license = await _context.Licenses.FindAsync(licenseId);
            if (license == null) return Result<bool>.Failure("License not found.");

            var currentUserId = _currentUserService.GetCurrentUserId();
            if (!currentUserId.HasValue) return Result<bool>.Failure("Current user not found.");

            try
            {
                license.Deactivate(currentUserId.Value);
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }

        public async Task<Result<List<LicenseDto>>> GetAllLicensesAsync()
        {
            var dtos = await _context.Licenses
                .AsNoTracking()
                .ProjectTo<LicenseDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Result<List<LicenseDto>>.Success(dtos);
        }

        public async Task<Result<LicenseDto>> GetLicenseByIdAsync(int licenseId)
        {
            var dto = await _context.Licenses
                .AsNoTracking()
                .ProjectTo<LicenseDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(l => l.Id == licenseId);

            if (dto == null) return Result<LicenseDto>.Failure("License not found.");

            return Result<LicenseDto>.Success(dto);
        }

        public async Task<Result<List<LicenseDto>>> GetLicensesByDriverIdAsync(int driverId)
        {
            var dtos = await _context.Licenses
                .Where(l => l.DriverID == driverId)
                .OrderByDescending(l => l.IssueDate)
                .AsNoTracking()
                .ProjectTo<LicenseDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Result<List<LicenseDto>>.Success(dtos);
        }

        public async Task<Result<bool>> IsLicenseValidForTransactionAsync(int licenseId)
        {
            var license = await _context.Licenses.FindAsync(licenseId);
            if (license == null) return Result<bool>.Failure("License not found.");

            bool isValid = license.IsActive && license.ExpirationDate > DateTime.UtcNow;
            return Result<bool>.Success(isValid);
        }

        public async Task<Result<int>> IssueLicenseFirstTimeAsync(CreateNewLicenseDto dto)
        {
            var currentUserId = _currentUserService.GetCurrentUserId();
            if (!currentUserId.HasValue) return Result<int>.Failure("Current user not found.");

            if (await _context.Licenses.AnyAsync(l => l.DriverID == dto.DriverID && l.LicenseClassID == dto.LicenseClassID && l.IsActive))
            {
                return Result<int>.Failure("Driver already has an active license of this class.");
            }

            var licenseClass = await _context.LicenseClasses.FindAsync(dto.LicenseClassID);
            if (licenseClass == null) return Result<int>.Failure("License Class not found.");

            var expirationDate = DateTime.UtcNow.AddYears(licenseClass.DefaultValidityLength);

            try
            {
                var license = new DVLD_Domain.Models.License(
                    dto.ApplicationID,
                    dto.DriverID,
                    dto.LicenseClassID,
                    expirationDate,
                    dto.Notes,
                    LicenseIssueReason.FirstTime,
                    dto.Fees,
                    currentUserId.Value
                );

                await _context.Licenses.AddAsync(license);
                
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

        public async Task<Result<int>> RenewLicenseAsync(int oldLicenseId, string notes)
        {
            var currentUserId = _currentUserService.GetCurrentUserId();
            if (!currentUserId.HasValue) return Result<int>.Failure("Current user not found.");

            var oldLicense = await _context.Licenses.FindAsync(oldLicenseId);
            if (oldLicense == null) return Result<int>.Failure("Old license not found.");

            if (!oldLicense.IsActive) return Result<int>.Failure("Old license is not active.");


            oldLicense.Deactivate(currentUserId.Value);

            var licenseClass = await _context.LicenseClasses.FindAsync(oldLicense.LicenseClassID);
            var expirationDate = DateTime.UtcNow.AddYears(licenseClass.DefaultValidityLength);

            try
            {
                var newLicense = new DVLD_Domain.Models.License(
                    oldLicense.ApplicationID,
                    oldLicense.DriverID,
                    oldLicense.LicenseClassID,
                    expirationDate,
                    notes,
                    LicenseIssueReason.Renewal,
                    licenseClass.ClassFees, 
                    currentUserId.Value
                );

                await _context.Licenses.AddAsync(newLicense);
                await _context.SaveChangesAsync();

                return Result<int>.Success(newLicense.Id);
            }
            catch (Exception ex)
            {
                return Result<int>.Failure(ex.Message);
            }
        }

        public async Task<Result<int>> ReplaceLicenseAsync(int oldLicenseId, LicenseIssueReason reason, string notes)
        {
            var currentUserId = _currentUserService.GetCurrentUserId();
            if (!currentUserId.HasValue) return Result<int>.Failure("Current user not found.");

            var oldLicense = await _context.Licenses.FindAsync(oldLicenseId);
            if (oldLicense == null) return Result<int>.Failure("Old license not found.");

            if (!oldLicense.IsActive) return Result<int>.Failure("Old license is not active.");


            oldLicense.Deactivate(currentUserId.Value);

            var expirationDate = oldLicense.ExpirationDate;

            try
            {
                var newLicense = new DVLD_Domain.Models.License(
                    oldLicense.ApplicationID,
                    oldLicense.DriverID,
                    oldLicense.LicenseClassID,
                    expirationDate,
                    notes,
                    reason,
                    0, 
                    currentUserId.Value
                );

                await _context.Licenses.AddAsync(newLicense);
                await _context.SaveChangesAsync();

                return Result<int>.Success(newLicense.Id);
            }
            catch (Exception ex)
            {
                return Result<int>.Failure(ex.Message);
            }
        }
    }
}
