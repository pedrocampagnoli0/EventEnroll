using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EventEnroll.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Title must be at least 8 characters.")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage = "Description cannot exceed 50 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Planner is required.")]
        public User Planner { get; set; }

        [Required(ErrorMessage = "At least one participant is required.")]
        public List<User> Participants { get; set; }
    }
}
