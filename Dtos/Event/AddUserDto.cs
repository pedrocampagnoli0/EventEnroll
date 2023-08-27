using System.ComponentModel.DataAnnotations;

namespace EventEnroll.Dtos.Event
{
    public class AddUserDto
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }
    }
}
