using AutoMapper;
using AutoMapper.QueryableExtensions;
using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Results;
using DVLD_Application.Services.Interfaces.Humans.User;
using DVLD_Application.Services.Interfaces.Password;
using DVLD_E_Enfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD_E_Enfrastructure.Service.Implementaions.Humans.User
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPasswordService _passwordService;

        public UserService(
            ApplicationDbContext context, 
            IMapper mapper, 
            ICurrentUserService currentUserService,
            IPasswordService passwordService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _passwordService = passwordService;
        }

        public async Task<Result<bool>> ActivateUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return Result<bool>.Failure("User not found.");

            var currentUserId = _currentUserService.GetCurrentUserId();
            if (!currentUserId.HasValue) return Result<bool>.Failure("Current user not found.");

            try
            {
                user.Activate(currentUserId.Value);
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }

        public async Task<Result<UserDto>> AuthenticateAsync(string userName, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == userName);
            if (user == null) return Result<UserDto>.Failure("Invalid username or password.");

            if (!_passwordService.VerifyPassword(user.PasswordHash, password))
            {
                return Result<UserDto>.Failure("Invalid username or password.");
            }

            if (!user.IsActive)
            {
                return Result<UserDto>.Failure("User is inactive.");
            }

            var dto = _mapper.Map<UserDto>(user);
            return Result<UserDto>.Success(dto);
        }

        public async Task<Result<bool>> ChangePasswordAsync(ChangePasswordDto dto)
        {
            var currentUserId = _currentUserService.GetCurrentUserId();
            if (!currentUserId.HasValue) return Result<bool>.Failure("Current user not found.");

            var user = await _context.Users.FindAsync(currentUserId.Value);
            if (user == null) return Result<bool>.Failure("User not found.");


            if (!_passwordService.VerifyPassword(user.PasswordHash, dto.oldPassword))
            {
                return Result<bool>.Failure("Invalid current password.");
            }

            try
            {
                var newHash = _passwordService.HashPassword(dto.newPassword);
                user.UpdatePassword(newHash, currentUserId.Value);
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }

        public async Task<Result<int>> CreateUserAsync(CreateNewUserDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.UserName == dto.UserName))
            {
                return Result<int>.Failure("Username already exists.");
            }
            if (await _context.Users.AnyAsync(u => u.Id == dto.Id))
            {
                return Result<int>.Failure("User already exists for this person.");
            }

            var currentUserId = _currentUserService.GetCurrentUserId();
            if(!currentUserId.HasValue) return Result<int>.Failure("Current User Not Found");

            try
            {
                var passwordHash = _passwordService.HashPassword(dto.Password);
                var user = new DVLD_Domain.Models.User(dto.Id, dto.UserName, passwordHash, currentUserId.Value);
                
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return Result<int>.Success(user.Id);
            }
            catch (Exception ex)
            {
                return Result<int>.Failure(ex.Message);
            }
        }

        public async Task<Result<bool>> DeactivateUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return Result<bool>.Failure("User not found.");

            var currentUserId = _currentUserService.GetCurrentUserId();
            if (!currentUserId.HasValue) return Result<bool>.Failure("Current user not found.");

            try
            {
                user.Deactivate(currentUserId.Value);
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }

        public async Task<Result<bool>> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return Result<bool>.Failure("User not found.");

            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to delete user: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Result<UserDto>>> GetAllUsersAsync()
        {
            var dtos = await _context.Users
                .AsNoTracking()
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return dtos.Select(d => Result<UserDto>.Success(d));
        }

        public async Task<Result<UserDto>> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null) return Result<UserDto>.Failure("User not found.");
            
            var dto = _mapper.Map<UserDto>(user);
            return Result<UserDto>.Success(dto);
        }

        public async Task<Result<UserDto>> GetUserByPersonIDAsync(int Id)
        {
             return await GetUserByPersonIDAsync(Id);
        }

        public async Task<Result<bool>> IsPersonUserAsync(int Id)
        {
            var exists = await _context.Users.AnyAsync(u => u.Id == Id);
            return Result<bool>.Success(exists);
        }

        public async Task<Result<bool>> IsUserNameExistAsync(string userName)
        {
            var exists = await _context.Users.AnyAsync(u => u.UserName == userName);
            return Result<bool>.Success(exists);
        }

        public async Task<Result<bool>> UpdateUserNameAsync(int userId, string newUserName)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return Result<bool>.Failure("User not found.");

            if (await _context.Users.AnyAsync(u => u.UserName == newUserName && u.Id != userId))
            {
                return Result<bool>.Failure("Username already taken.");
            }

            var currentUserId = _currentUserService.GetCurrentUserId();
            if (!currentUserId.HasValue) return Result<bool>.Failure("Current user not found.");

            try
            {
                user.UpdateUserName(newUserName, currentUserId.Value);
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }
    }
}
