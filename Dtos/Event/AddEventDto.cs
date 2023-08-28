using EventEnroll.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventEnroll.Dtos.Event
{
    public class AddEventDto
    {

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        [JsonIgnore]
        public string? CreatorId { get; set; }
        [Required]
        public List<string> Attendees { get; set; } = new List<string>();

    }
}
