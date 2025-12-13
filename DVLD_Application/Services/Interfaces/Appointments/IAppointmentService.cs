using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Dtos.UpdateDtos;
using DVLD_Application.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD_Application.Services.Interfaces.Appointments
{
    public interface IAppointmentService
    {
        Task<Result<AppointmentDto>> GetAppointmentByIdAsync(int appointmentId);

       
        Task<Result<List<AppointmentDto>>> GetAppointmentsByLocalAppIdAsync(int localDrivingLicenseApplicationId, int testTypeId);

       
        Task<Result<bool>> HasActiveAppointmentAsync(int localDrivingLicenseApplicationId, int testTypeId);

        
        Task<Result<bool>> HasPassedTestAsync(int localDrivingLicenseApplicationId, int testTypeId);

        
        Task<Result<int>> CreateAppointmentAsync(CreateNewAppointmentDto dto);
        Task<Result<bool>> RescheduleAppointmentAsync(UpdateAppointmentDateDto dto);
        Task<Result<bool>> DeleteAppointmentAsync(int appointmentId);
    }
}
