using DVLD_Domain.Enums;
using DVLD_Application.Results;
using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD_Application.Services.Interfaces.Licenses.Local
{
    public interface ILicenseService
    {
        Task<Result<LicenseDto>> GetLicenseByIdAsync(int licenseId);
        Task<Result<List<LicenseDto>>> GetAllLicensesAsync();
        Task<Result<List<LicenseDto>>> GetLicensesByDriverIdAsync(int driverId);

        
        Task<Result<bool>> IsLicenseValidForTransactionAsync(int licenseId);


       
        Task<Result<int>> IssueLicenseFirstTimeAsync(CreateNewLicenseDto newLicense);

       
        Task<Result<int>> RenewLicenseAsync(int oldLicenseId, string notes);

        
        Task<Result<int>> ReplaceLicenseAsync(int oldLicenseId, LicenseIssueReason reason, string notes);

       
        Task<Result<bool>> DeactivateLicenseAsync(int licenseId);

        Task<Result<bool>> ActivateLicenseAsync(int licenseId);

    }
}
