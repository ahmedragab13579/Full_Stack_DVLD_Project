using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD_Application.Services.Interfaces.Applications
{
    public interface ILocalDrivingLicenseApplicationService
    {
        Task<Result<LocalDrivingLicenseApplicationDto>> GetLocalDrivingLicenseApplicationByIdAsync(int localAppId);

        Task<Result<LocalDrivingLicenseApplicationDto>> GetByApplicationIdAsync(int applicationId);

        Task<Result<List<LocalDrivingLicenseApplicationDto>>> GetAllApplicationsAsync();

        Task<Result<bool>> DidPassAllTestsAsync(int localAppId);

        Task<Result<bool>> IsLicenseIssuedAsync(int localAppId);

        Task<Result<bool>> HasActiveApplicationForClassAsync(int Id, int licenseClassId);


       
        Task<Result<int>> CreateNewApplicationAsync(CreateNewLocalDrivingLicenseApplicationDto dto);
        Task<Result<bool>> CancelApplicationAsync(int localAppId);
        Task<Result<bool>> DeleteApplicationAsync(int localAppId);


    }
}
