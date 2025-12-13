using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD_Application.Services.Interfaces.Licenses.Detain
{
    public interface IDetainLicenseService
    {
        Task<Result<DetainedLicenseDto>> GetDetainedLicenseByIdAsync(int detainId);

       
        Task<Result<DetainedLicenseDto>> GetActiveDetainedLicenseByLicenseIdAsync(int licenseId);

        Task<Result<List<Result<DetainedLicenseDto>>>> GetAllDetainedLicensesAsync();

        Task<Result<bool>> IsLicenseDetainedAsync(int licenseId);


        
        Task<Result<int>> DetainLicenseAsync(CreateNewDetainedLicenseDto dto);

       
        Task<bool> ReleaseLicenseAsync(CreateNewReleaseLicenseDto createNewRelease);

    }
}
