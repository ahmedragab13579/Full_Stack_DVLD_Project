using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD_Application.Services.Interfaces.Humans.User
{
    public interface IUserService
    {
        Task<Result<UserDto>> AuthenticateAsync(string userName, string password);


      
        Task<Result<UserDto>> GetUserByIdAsync(int userId);

       
        Task<Result<UserDto>> GetUserByPersonIdAsync(int personId);

        Task<IEnumerable<Result<UserDto>>> GetAllUsersAsync();

        Task<Result<bool>> IsUserNameExistAsync(string userName);

        Task<Result<bool>> IsPersonUserAsync(int personId);


        Task<Result<int>> CreateUserAsync(CreateNewUserDto dto);

       
        Task<Result<bool>> ChangePasswordAsync(ChangePasswordDto dto);

        Task<Result<bool>> UpdateUserNameAsync(int userId, string newUserName);

        Task<Result<bool>> ActivateUserAsync(int userId);
        Task<Result<bool>> DeactivateUserAsync(int userId);

        Task<Result<bool>> DeleteUserAsync(int userId);
    }
}
  
