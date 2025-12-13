using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Dtos.UpdateDtos;
using DVLD_Application.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD_Application.Services.Interfaces.Tests
{
    public interface ITestTypeService
    {
        Task<Result<List<TestTypeDto>>> GetAllTestTypesAsync();

        Task<Result<TestTypeDto>> GetTestTypeByIdAsync(int id);
     
        Task<Result<bool>> UpdateTestTypeAsync(UpdateTestTypeDto dto);

    }
}
