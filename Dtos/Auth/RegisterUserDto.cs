using System.ComponentModel.DataAnnotations;

namespace EventEnroll.Dtos.Auth
{
    public class RegisterUserDto
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }
    }
}
