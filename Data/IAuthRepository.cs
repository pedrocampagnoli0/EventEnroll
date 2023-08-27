using EventEnroll.Dtos.Auth;
using Microsoft.AspNetCore.Identity;

namespace EventEnroll.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<string>> Register(RegisterUserDto user);
        Task<ServiceResponse<String>> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
