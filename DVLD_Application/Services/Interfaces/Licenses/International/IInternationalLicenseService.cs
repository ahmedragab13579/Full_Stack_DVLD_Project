using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD_Application.Services.Interfaces.Licenses.International
{
    public interface IInternationalLicenseService
    {
        Task<Result<InternationalLicenseDto>> GetInternationalLicenseByIdAsync(int internationalLicenseId);

        Task<Result< List<InternationalLicenseDto>>> GetInternationalLicensesByDriverIdAsync(int driverId);

        Task<Result <List<InternationalLicenseDto>>> GetAllInternationalLicensesAsync();
      
        Task<Result< bool>> HasActiveInternationalLicenseAsync(int driverId);
      
        Task<Result <int>> IssueInternationalLicenseAsync(CreateNewInternationalLicenseDto dto);
    }
}
