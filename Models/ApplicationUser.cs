using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventEnroll.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Key]
        public string? Id { get; set; }
        // Navigation properties
        public ICollection<Event>? Events { get; set; }
        // Additional properties
        public ICollection<Event>? CreatedEvents { get; set; }
    }
} 