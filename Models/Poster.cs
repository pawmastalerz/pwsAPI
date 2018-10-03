using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pwsAPI.Models
{
    public class Poster
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        
        [Required]
        public DateTime HappensAt { get; set; }

        [Required]
        public string PosterPhotoUrl { get; set; }
    }
}