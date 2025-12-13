using AutoMapper;
using AutoMapper.QueryableExtensions;
using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Results;
using DVLD_Application.Services.Interfaces.Humans.Driver;
using DVLD_E_Enfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_E_Enfrastructure.Service.Implementaions.Humans.Driver
{
    public class DriverService : IDriverService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly DVLD_Application.Services.Interfaces.Humans.User.ICurrentUserService _currentUserService;

        public DriverService(ApplicationDbContext context, IMapper mapper, DVLD_Application.Services.Interfaces.Humans.User.ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Result<int>> CreateDriverAsync(CreateNewDriverDto dto)
        {
            if (await _context.Drivers.AnyAsync(d => d.PersonID == dto.PersonID))
            {
                return Result<int>.Failure("Driver already exists for this person.");
            }

            var currentUserId = _currentUserService.GetCurrentUserId();
            if (!currentUserId.HasValue) return Result<int>.Failure("Current User not found");

            try
            {
                var driver = new DVLD_Domain.Models.Driver(dto.PersonID, currentUserId.Value);
                await _context.Drivers.AddAsync(driver);
                await _context.SaveChangesAsync();
                return Result<int>.Success(driver.PersonID); 
            }
            catch (Exception ex)
            {
                return Result<int>.Failure(ex.Message);
            }
        }

        public async Task<Result<List<DriverDto>>> GetAllDriversAsync()
        {
            var dtos = await _context.Drivers
                .AsNoTracking()
                .ProjectTo<DriverDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Result<List<DriverDto>>.Success(dtos);
        }

        public async Task<Result<DriverDto>> GetDriverByIdAsync(int driverId)
        {
            var driver = await _context.Drivers
                .Include(d => d.Person)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.PersonID == driverId);

            if (driver == null) return Result<DriverDto>.Failure("Driver not found.");

            var dto = _mapper.Map<DriverDto>(driver);
            return Result<DriverDto>.Success(dto);
        }

        public async Task<Result<DriverDto>> GetDriverByNationalNoAsync(string nationalNo)
        {
            var driver = await _context.Drivers
                .Include(d => d.Person)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Person.NationalNo == nationalNo);

            if (driver == null) return Result<DriverDto>.Failure("Driver not found.");

            var dto = _mapper.Map<DriverDto>(driver);
            return Result<DriverDto>.Success(dto);
        }

        public async Task<Result<DriverDto>> GetDriverByPersonIdAsync(int personId)
        {
            return await GetDriverByIdAsync(personId);
        }

        public async Task<Result<bool>> IsPersonDriverAsync(int personId)
        {
            var exists = await _context.Drivers.AnyAsync(d => d.PersonID == personId);
            return Result<bool>.Success(exists);
        }
    }
}
