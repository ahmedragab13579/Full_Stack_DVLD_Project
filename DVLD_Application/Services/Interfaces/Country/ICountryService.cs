using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD_Application.Services.Interfaces.Country
{
    public interface ICountryService
    {
        Task<Result<List<CountryDto>>> GetAllCountriesAsync();
        Task<Result<CountryDto>> GetCountryByIdAsync(int countryId);
    }
}
