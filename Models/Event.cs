using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace EventEnroll.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Title must be at least 8 characters.")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage = "Description cannot exceed 50 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        public string? CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        public IdentityUser? Creator { get; set; }
        // Navigation properties
        public ICollection<IdentityUser>? Attendees { get; set; } = new List<IdentityUser>();
    }
}
