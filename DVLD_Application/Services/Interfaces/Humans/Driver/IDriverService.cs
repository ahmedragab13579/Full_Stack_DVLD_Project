using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD_Application.Services.Interfaces.Humans.Driver
{
    public interface IDriverService
    {
        Task<Result<DriverDto>> GetDriverByIdAsync(int driverId);

        Task<Result<DriverDto>> GetDriverByPersonIdAsync(int personId);

        Task<Result<DriverDto>> GetDriverByNationalNoAsync(string nationalNo);

        Task<Result < List<DriverDto>>> GetAllDriversAsync();

        Task<Result<bool>> IsPersonDriverAsync(int personId);

        Task<Result<int>> CreateDriverAsync(CreateNewDriverDto dto);
    }
}
