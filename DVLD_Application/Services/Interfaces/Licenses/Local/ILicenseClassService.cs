using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Dtos.UpdateDtos;
using DVLD_Application.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD_Application.Services.Interfaces.Licenses.Local
{
    public interface ILicenseClassService
    {
        Task<Result< List<LicenseClassDto>>> GetAllLicenseClassesAsync();

        Task<Result< LicenseClassDto>> GetLicenseClassByIdAsync(int licenseClassId);

        Task<Result< LicenseClassDto>> GetLicenseClassByClassNameAsync(string className);

        Task<Result< int>> CreateLicenseClassAsync(CreateNewLicenseClassDto dto);

        Task<Result <bool>> UpdateLicenseClassAsync(UpdateLicenseClassDto dto);

        Task<Result< bool>> DeleteLicenseClassAsync(int licenseClassId);
    }
}
