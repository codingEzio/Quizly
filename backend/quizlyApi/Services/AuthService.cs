using quizlyApi.DTOs;
using quizlyApi.Models;
using quizlyApi.Services;

namespace quizlyApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _userService.GetByNameAsync(registerDto.Name);
            if (existingUser is not null)
            {
                throw new InvalidOperationException("User with this name already exists.");
            }

            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = registerDto.Password // Storing password as plain text
            };

            var createdUser = await _userService.CreateAsync(user);

            return new UserDto
            {
                Id = createdUser.Id,
                Name = createdUser.Name,
                Email = createdUser.Email
            };
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userService.GetByNameAsync(loginDto.Name);
            if (user is null || user.Password != loginDto.Password)
            {
                throw new InvalidOperationException("Invalid name or password.");
            }

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}