using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Dtos.UpdateDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD_Application.Services.Interfaces.Tests
{
    public interface ITestService
    {
        Task<TestDto> GetTestByIdAsync(int testId);
        Task<List<TestDto>> GetAllTestsAsync();
        Task<TestDto> GetTestByAppointmentIdAsync(int appointmentId);

       
        Task<TestDto> GetLastTestByPersonAndTestTypeAsync(int Id, int licenseClassId, int testTypeId);

        Task<byte> GetPassedTestCountAsync(int localDrivingLicenseApplicationId);

        Task<bool> IsTestPassedAsync(int localDrivingLicenseApplicationId, int testTypeId);


      
        Task<int> CreateTestAsync(CreateNewTestDto dto);

        
        Task<bool> TakeTestAsync(UpdateTestResultDto dto);
    }
}
