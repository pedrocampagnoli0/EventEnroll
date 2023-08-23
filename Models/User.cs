using Microsoft.AspNetCore.Identity;

namespace EventEnroll.Models
{
    public class User : IdentityUser
    {
        public List<Event> Events { get; set; } = new List<Event>();
    }
}
