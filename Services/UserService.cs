using EcommerceAPI.Models;
using EcommerceAPI.Models.DTO;
using EcommerceAPI.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _config;

        public UserService(IUserRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _repository.GetByEmailAsync(request.Email);
            if (user == null || user.PasswordHash != request.Password) return null;

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Role = user.Role
            };
        }

        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            var existing = await _repository.GetByEmailAsync(request.Email);
            if (existing != null) return false;

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = request.Password,
                Phone = request.Phone,
                Address = request.Address,
                Role = request.Role,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<UserProfileResponse?> GetProfileAsync(int userId)
        {
            var user = await _repository.GetByIdAsync(userId);
            if (user == null) return null;

            return new UserProfileResponse
            {
                UserID = user.UserID,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                Role = user.Role,
                IsActive = user.IsActive
            };
        }

        public async Task<bool> UpdateProfileAsync(int userId, UpdateProfileRequest request)
        {
            var user = await _repository.GetByIdAsync(userId);
            if (user == null) return false;

            user.FullName = request.FullName;
            user.Phone = request.Phone;
            user.Address = request.Address;

            if (!string.IsNullOrEmpty(request.Password))
            {
                user.PasswordHash = request.Password;
            }

            await _repository.UpdateAsync(user);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
