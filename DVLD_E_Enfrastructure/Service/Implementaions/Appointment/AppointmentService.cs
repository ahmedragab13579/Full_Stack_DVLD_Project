using AutoMapper;
using AutoMapper.QueryableExtensions;
using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Dtos.UpdateDtos;
using DVLD_Application.Results;
using DVLD_Application.Services.Interfaces.Appointments;
using DVLD_Application.Services.Interfaces.Mapping;
using DVLD_E_Enfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_E_Enfrastructure.Service.Implementaions.Appointment
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly DVLD_Application.Services.Interfaces.Humans.User.ICurrentUserService _currentUserService;

        public AppointmentService(ApplicationDbContext context, IMapper mapper, DVLD_Application.Services.Interfaces.Humans.User.ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Result<int>> CreateAppointmentAsync(CreateNewAppointmentDto dto)
        {            if (await HasActiveAppointmentAsync(dto.LocalDrivingLicenseApplicationID, dto.TestTypeID).ContinueWith(t => t.Result.Data))
            {
                return Result<int>.Failure("Person already has an active appointment for this test.");
            }

            if (await HasPassedTestAsync(dto.LocalDrivingLicenseApplicationID, dto.TestTypeID).ContinueWith(t => t.Result.Data))
            {
                return Result<int>.Failure("Person already passed this test.");
            }

            var currentUserId = _currentUserService.GetCurrentUserId();
            if(!currentUserId.HasValue) return Result<int>.Failure("Current User Not Found");

            try
            {
                var appointment = new DVLD_Domain.Models.Appointment(
                    dto.TestTypeID,
                    dto.LocalDrivingLicenseApplicationID,
                    dto.AppointmentDate,
                    dto.PaidFees,
                    currentUserId.Value
                );

                await _context.Appointments.AddAsync(appointment);
                await _context.SaveChangesAsync();

                return Result<int>.Success(appointment.Id);
            }
            catch (Exception ex)
            {
                return Result<int>.Failure(ex.Message);
            }
        }

        public async Task<Result<bool>> DeleteAppointmentAsync(int appointmentId)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment == null) return Result<bool>.Failure("Appointment not found.");

            if (appointment.IsLocked)
            {
                return Result<bool>.Failure("Cannot delete a locked appointment.");
            }

            try
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }

        public async Task<Result<AppointmentDto>> GetAppointmentByIdAsync(int appointmentId)
        {
            var appointment = await _context.Appointments
                .Include(a => a.TestType)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == appointmentId);

            if (appointment == null) return Result<AppointmentDto>.Failure("Appointment not found.");

            var dto = _mapper.Map<AppointmentDto>(appointment);
            return Result<AppointmentDto>.Success(dto);
        }

        public async Task<Result<List<AppointmentDto>>> GetAppointmentsByLocalAppIdAsync(int localDrivingLicenseApplicationId, int testTypeId)
        {
            var dtos = await _context.Appointments
                .Where(a => a.LocalDrivingLicenseApplicationID == localDrivingLicenseApplicationId && a.TestTypeID == testTypeId)
                .OrderByDescending(a => a.AppointmentDate)
                .AsNoTracking()
                .ProjectTo<AppointmentDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Result<List<AppointmentDto>>.Success(dtos);
        }

        public async Task<Result<bool>> HasActiveAppointmentAsync(int localDrivingLicenseApplicationId, int testTypeId)
        {
            var hasActive = await _context.Appointments
                .AnyAsync(a => a.LocalDrivingLicenseApplicationID == localDrivingLicenseApplicationId 
                            && a.TestTypeID == testTypeId 
                            && !a.IsLocked);
            return Result<bool>.Success(hasActive);
        }

        public async Task<Result<bool>> HasPassedTestAsync(int localDrivingLicenseApplicationId, int testTypeId)
        {
            var hasPassed = await _context.Tests
                .Include(t => t.Appointment)
                .AnyAsync(t => t.Appointment.LocalDrivingLicenseApplicationID == localDrivingLicenseApplicationId
                            && t.Appointment.TestTypeID == testTypeId
                            && t.TestResult == true);
            
            return Result<bool>.Success(hasPassed);
        }

        public async Task<Result<bool>> RescheduleAppointmentAsync(UpdateAppointmentDateDto dto)
        {
            var appointment = await _context.Appointments.FindAsync(dto.AppointmentId);
            if (appointment == null) return Result<bool>.Failure("Appointment not found.");

            var currentUserId = _currentUserService.GetCurrentUserId();
            if (!currentUserId.HasValue) return Result<bool>.Failure("Current User Not Found");

            try
            {
                appointment.UpdateAppointmentDate(dto.NewAppointmentDate, currentUserId.Value);
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
