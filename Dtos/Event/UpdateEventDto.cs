using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEnroll.Dtos.Event
{
    public class UpdateEventDto
    {
        public int EventId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string? CreatorId { get; set; }
        public ApplicationUser? Creator { get; set; }
        public ICollection<ApplicationUser>? Attendees { get; set; } = new List<ApplicationUser>();
    }
}
