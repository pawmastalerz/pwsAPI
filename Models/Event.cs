using System;
using System.Collections.Generic;

namespace pwsAPI.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public DateTime HappensAt { get; set; }
        public string PosterPhotoUrl { get; set; }
        public string SignUpLink { get; set; }
        public short Accepted { get; set; }
        public string Story { get; set; }

        public List<EventPhoto> EventPhoto { get; set; }
    }
}