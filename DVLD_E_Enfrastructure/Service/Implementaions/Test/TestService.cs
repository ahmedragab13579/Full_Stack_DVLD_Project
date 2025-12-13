using AutoMapper;
using AutoMapper.QueryableExtensions;
using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Dtos.UpdateDtos;
using DVLD_Application.Services.Interfaces.Humans.User;
using DVLD_Application.Services.Interfaces.Tests;
using DVLD_E_Enfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_E_Enfrastructure.Service.Implementaions.Test
{
    public class TestService : ITestService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public TestService(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<int> CreateTestAsync(CreateNewTestDto dto)
        {
            var currentUserId = _currentUserService.GetCurrentUserId();
            if (!currentUserId.HasValue) throw new UnauthorizedAccessException("User not found.");

            // Check if test already exists for this appointment
            if (await _context.Tests.AnyAsync(t => t.AppointmentID == dto.AppointmentID))
            {
                throw new InvalidOperationException("Test already exists for this appointment.");
            }

            var test = new DVLD_Domain.Models.Test(dto.AppointmentID, currentUserId.Value);
            await _context.Tests.AddAsync(test);
            await _context.SaveChangesAsync();

            return test.AppointmentID;
        }

        public async Task<TestDto> GetLastTestByPersonAndTestTypeAsync(int personId, int licenseClassId, int testTypeId)
        {
            var dto = await _context.Tests
                .Where(t => t.Appointment.LocalDrivingLicenseApplication.Application.PersonID == personId
                            && t.Appointment.LocalDrivingLicenseApplication.LicenseClassID == licenseClassId
                            && t.Appointment.TestTypeID == testTypeId)
                .OrderByDescending(t => t.Appointment.AppointmentDate)
                .AsNoTracking()
                .ProjectTo<TestDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return dto;
        }

        public async Task<byte> GetPassedTestCountAsync(int localDrivingLicenseApplicationId)
        {
            var count = await _context.Tests
                .Where(t => t.Appointment.LocalDrivingLicenseApplicationID == localDrivingLicenseApplicationId
                            && t.TestResult == true)
                .CountAsync();

            return (byte)count;
        }

        public async Task<TestDto> GetTestByAppointmentIdAsync(int appointmentId)
        {
            var dto = await _context.Tests
                .AsNoTracking()
                .Where(t => t.AppointmentID == appointmentId)
                .ProjectTo<TestDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return dto;
        }

        public async Task<TestDto> GetTestByIdAsync(int testId)
        {
            // TestID is AppointmentID
            return await GetTestByAppointmentIdAsync(testId);
        }

        public async Task<bool> IsTestPassedAsync(int localDrivingLicenseApplicationId, int testTypeId)
        {
            return await _context.Tests
                .AnyAsync(t => t.Appointment.LocalDrivingLicenseApplicationID == localDrivingLicenseApplicationId
                            && t.Appointment.TestTypeID == testTypeId
                            && t.TestResult == true);
        }

        public async Task<bool> TakeTestAsync(UpdateTestResultDto dto)
        {
            var test = await _context.Tests.FindAsync(dto.Id);
            if (test == null) return false;

            var currentUserId = _currentUserService.GetCurrentUserId();
            if (!currentUserId.HasValue) throw new UnauthorizedAccessException("User not found.");

            try
            {
                // Notes are missing in DTO, passing empty string or null
                test.UpdateTestResult(dto.IsPassed, null, currentUserId.Value);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
