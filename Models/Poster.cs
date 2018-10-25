using System;

namespace pwsAPI.Models
{
    public class Poster
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime HappensAt { get; set; }
        public short Accepted { get; set; }
        public string PosterPhotoUrl { get; set; }
    }
}