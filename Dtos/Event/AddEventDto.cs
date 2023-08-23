using EventEnroll.Models;
using System.ComponentModel.DataAnnotations;

namespace EventEnroll.Dtos.Event
{
    public class AddEventDto
    {

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public User Planner { get; set; }
        public List<User> Participants { get; set; }
    }
}
