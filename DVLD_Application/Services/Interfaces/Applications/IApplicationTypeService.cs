
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Dtos.UpdateDtos;
using DVLD_Application.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD_Application.Services.Interfaces.Applications
{
    public interface IApplicationTypeService
    {
        Task<Result<List<ApplicationTypeDto>>> GetAllAsync();
        Task<Result<ApplicationTypeDto>> GetByIdAsync(int id);
        Task<Result<bool>> UpdateAsync(UpdateApplicationTypeDto dto,int userid);
    }
}
