using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Dtos.UpdateDtos;
using DVLD_Application.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD_Application.Services.Interfaces.Humans.Person
{
    public interface IPersonService
    {
        Task<Result<PersonDto>> GetPersonByIdAsync(int Id);

        Task<Result<PersonDto>> GetPersonByNationalNoAsync(string nationalNo);

        Task<Result<List<PersonDto>>> GetAllPeopleAsync();

        Task<Result<bool>> IsPersonExistAsync(string nationalNo);
        Task<Result<bool>> IsPersonExistAsync(int Id);


      
        Task<Result<int>> CreatePersonAsync(CreateNewPersonDto dto);

       
        Task<Result<bool>> UpdatePersonAsync(UpdatePersonDto dto);

      
        Task<Result<bool>> DeletePersonAsync(int Id);
    }
}
