using AutoMapper;
using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Results;
using DVLD_Application.Services.Interfaces.Licenses.Detain;
using DVLD_E_Enfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD_E_Enfrastructure.Service.Implementaions.License.Detain
{
    public class DetainLicenseService : IDetainLicenseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DetainLicenseService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<int>> DetainLicenseAsync(CreateNewDetainedLicenseDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<DetainedLicenseDto>> GetActiveDetainedLicenseByLicenseIdAsync(int licenseId)
        {
            try
            {
                var entity = await _context.DetainedLicenses
                    .Include(dl => dl.License)
                        .ThenInclude(l => l.Driver)
                            .ThenInclude(d => d.Person)
                    .Include(dl => dl.ReleasedByUser)
                    .Include(dl => dl.ReleaseApplication)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(dl => dl.LicenseID == licenseId && !dl.IsReleased);

                if (entity == null)
                    return Result<DetainedLicenseDto>.Failure("No active detention found for this license.");

                return Result<DetainedLicenseDto>.Success(_mapper.Map<DetainedLicenseDto>(entity));
            }
            catch (Exception ex)
            {
                return Result<DetainedLicenseDto>.Failure($"Error retrieving active detention: {ex.Message}");
            }
        }

        public async Task<Result<List<Result<DetainedLicenseDto>>>> GetAllDetainedLicensesAsync()
        {
            try
            {
                var entities = await _context.DetainedLicenses
                    .Include(dl => dl.License)
                        .ThenInclude(l => l.Driver)
                            .ThenInclude(d => d.Person)
                    .Include(dl => dl.ReleasedByUser)
                    .Include(dl => dl.ReleaseApplication)
                    .AsNoTracking()
                    .ToListAsync();

                var results = entities
                    .Select(dl => Result<DetainedLicenseDto>.Success(_mapper.Map<DetainedLicenseDto>(dl)))
                    .ToList();

                return Result<List<Result<DetainedLicenseDto>>>.Success(results);
            }
            catch (Exception ex)
            {
                return Result<List<Result<DetainedLicenseDto>>>.Failure($"Error retrieving detained licenses: {ex.Message}");
            }
        }

        public async Task<Result<DetainedLicenseDto>> GetDetainedLicenseByIdAsync(int detainId)
        {
            try
            {
                var entity = await _context.DetainedLicenses
                    .Include(dl => dl.License)
                        .ThenInclude(l => l.Driver)
                            .ThenInclude(d => d.Person)
                    .Include(dl => dl.ReleasedByUser)
                    .Include(dl => dl.ReleaseApplication)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(dl => dl.Id == detainId);

                if (entity == null)
                    return Result<DetainedLicenseDto>.Failure("Detained license record not found.");

                return Result<DetainedLicenseDto>.Success(_mapper.Map<DetainedLicenseDto>(entity));
            }
            catch (Exception ex)
            {
                return Result<DetainedLicenseDto>.Failure($"Error retrieving detained license by ID: {ex.Message}");
            }
        }

        public async Task<Result<bool>> IsLicenseDetainedAsync(int licenseId)
        {
            try
            {
                var isDetained = await _context.DetainedLicenses.AnyAsync(dl => dl.LicenseID == licenseId && !dl.IsReleased);
                return Result<bool>.Success(isDetained);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error checking if license is detained: {ex.Message}");
            }
        }

        public async Task<bool> ReleaseLicenseAsync(CreateNewReleaseLicenseDto createNewRelease)
        {
            throw new NotImplementedException();
        }
    }
}
