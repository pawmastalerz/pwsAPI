using System;

namespace pwsAPI.Dtos
{
    public class PosterForUpdateDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime HappensAt { get; set; }
        public string PosterPhotoUrl { get; set; }
    }
}