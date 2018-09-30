using System;

namespace pwsAPI.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime HappensAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PhotoUrl { get; set; }

    }
}