using DVLD_Application.Results;
using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Dtos.UpdateDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Services.Interfaces.Applications
{
    public interface IApplicationService
    {



        Task<Result< bool>> CancelApplicationAsync(int applicationId);
        Task<Result <bool>> CompleteApplicationAsync(int applicationId);
        Task<Result<int>> AddAsync(CreateNewApplicationDto newApplicationDto);
        Task<Result<ApplicationDto>> GetByIdAsync(int id);
        Task<Result<List<ApplicationDto>>> GetAllAsync();
        Task<Result<bool>> IsPersonHasActiveApplicationAsync(int personId, int applicationtypeid);
    }
}
