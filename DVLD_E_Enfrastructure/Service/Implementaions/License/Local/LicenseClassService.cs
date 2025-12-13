using AutoMapper;
using AutoMapper.QueryableExtensions;
using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Dtos.UpdateDtos;
using DVLD_Application.Results;
using DVLD_Application.Services.Interfaces.Humans.User;
using DVLD_Application.Services.Interfaces.Licenses.Local;
using DVLD_E_Enfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_E_Enfrastructure.Service.Implementaions.License.Local
{
    public class LicenseClassService : ILicenseClassService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public LicenseClassService(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Result<int>> CreateLicenseClassAsync(CreateNewLicenseClassDto dto)
        {
            var currentUserId = _currentUserService.GetCurrentUserId();
            if (!currentUserId.HasValue) return Result<int>.Failure("Current user not found.");

            if (await _context.LicenseClasses.AnyAsync(lc => lc.ClassName == dto.ClassName))
            {
                return Result<int>.Failure("License Class with this name already exists.");
            }

            try
            {
                var licenseClass = new DVLD_Domain.Models.LicenseClass(
                    dto.ClassName,
                    dto.ClassDescription,
                    dto.MinimumAllowedAge,
                    dto.DefaultValidityLength,
                    dto.ClassFees,
                    currentUserId.Value
                );

                await _context.LicenseClasses.AddAsync(licenseClass);
                await _context.SaveChangesAsync();

                return Result<int>.Success(licenseClass.Id);
            }
            catch (Exception ex)
            {
                return Result<int>.Failure(ex.Message);
            }
        }

        public async Task<Result<bool>> DeleteLicenseClassAsync(int licenseClassId)
        {
            var licenseClass = await _context.LicenseClasses.FindAsync(licenseClassId);
            if (licenseClass == null) return Result<bool>.Failure("License Class not found.");

            try
            {
                _context.LicenseClasses.Remove(licenseClass);
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }

        public async Task<Result<List<LicenseClassDto>>> GetAllLicenseClassesAsync()
        {
            var dtos = await _context.LicenseClasses
                .AsNoTracking()
                .ProjectTo<LicenseClassDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Result<List<LicenseClassDto>>.Success(dtos);
        }

        public async Task<Result<LicenseClassDto>> GetLicenseClassByClassNameAsync(string className)
        {
            var dto = await _context.LicenseClasses
                .AsNoTracking()
                .ProjectTo<LicenseClassDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(lc => lc.ClassName == className);

            if (dto == null) return Result<LicenseClassDto>.Failure("License Class not found.");

            return Result<LicenseClassDto>.Success(dto);
        }

        public async Task<Result<LicenseClassDto>> GetLicenseClassByIdAsync(int licenseClassId)
        {
            var dto = await _context.LicenseClasses
                .AsNoTracking()
                .ProjectTo<LicenseClassDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(lc => lc.Id == licenseClassId);

            if (dto == null) return Result<LicenseClassDto>.Failure("License Class not found.");

            return Result<LicenseClassDto>.Success(dto);
        }

        public async Task<Result<bool>> UpdateLicenseClassAsync(UpdateLicenseClassDto dto)
        {
            var licenseClass = await _context.LicenseClasses.FindAsync(dto.Id);
            if (licenseClass == null) return Result<bool>.Failure("License Class not found.");

            var currentUserId = _currentUserService.GetCurrentUserId();
            if (!currentUserId.HasValue) return Result<bool>.Failure("Current user not found.");

            try
            {
                licenseClass.Updateinformation(
                    dto.ClassName,
                    dto.ClassDescription,
                    dto.MinimumAllowedAge,
                    dto.DefaultValidityLength,
                    dto.ClassFees,
                    currentUserId.Value
                );

                await _context.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }
    }
}
