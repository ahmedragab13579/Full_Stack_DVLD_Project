using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Services.Interfaces.Humans.User;
using Microsoft.AspNetCore.Mvc;

namespace DVLD_Persentation.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>A list of users.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var results = await _userService.GetAllUsersAsync();
            var users = results.Select(r => r.Data).ToList();
            return Ok(users);
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>The user details if found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="dto">The data to create the user.</param>
        /// <returns>The ID of the created user.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateNewUserDto dto)
        {
            var result = await _userService.CreateUserAsync(dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return CreatedAtAction(nameof(GetUserById), new { id = result.Data }, result.Data);
        }

        /// <summary>
        /// Changes the password for a user.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <param name="dto">The password change details.</param>
        /// <returns>A success message.</returns>
        [HttpPut("{id}/password")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordDto dto)
        {   
            var result = await _userService.ChangePasswordAsync(dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok("Password changed successfully.");
        }

        /// <summary>
        /// Updates the username of a user.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <param name="newUserName">The new username.</param>
        /// <returns>A success message.</returns>
        [HttpPut("{id}/username")]
        public async Task<IActionResult> UpdateUserName(int id, [FromBody] string newUserName)
        {
            var result = await _userService.UpdateUserNameAsync(id, newUserName);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok("Username updated successfully.");
        }

        /// <summary>
        /// Activates or Deactivates a user.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <param name="isActive">The new activation status.</param>
        /// <returns>A success message.</returns>
        [HttpPut("{id}/activation")]
        public async Task<IActionResult> UpdateActivation(int id, [FromBody] bool isActive)
        {
            if (isActive)
            {
                var result = await _userService.ActivateUserAsync(id);
                if (!result.IsSuccess) return BadRequest(result.Message);
            }
            else
            {
                var result = await _userService.DeactivateUserAsync(id);
                if (!result.IsSuccess) return BadRequest(result.Message);
            }
            return Ok($"User {(isActive ? "activated" : "deactivated")} successfully.");
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A success message.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok("User deleted successfully.");
        }
    }
}
