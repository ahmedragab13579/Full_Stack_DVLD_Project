using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Results;
using DVLD_Application.Services.Interfaces.Licenses.Detain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_E_Enfrastructure.Service.Implementaions.License.Detain
{
    public class DetainLicenseService : IDetainLicenseService
    {
        public Task<Result<int>> DetainLicenseAsync(CreateNewDetainedLicenseDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<Result<DetainedLicenseDto>> GetActiveDetainedLicenseByLicenseIdAsync(int licenseId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<Result<DetainedLicenseDto>>>> GetAllDetainedLicensesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<DetainedLicenseDto>> GetDetainedLicenseByIdAsync(int detainId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> IsLicenseDetainedAsync(int licenseId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ReleaseLicenseAsync(CreateNewReleaseLicenseDto createNewRelease)
        {
            throw new NotImplementedException();
        }
    }
}
