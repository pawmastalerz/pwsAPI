namespace pwsAPI.Models
{
    public class EventPhoto
    {
        public int EventPhotoId { get; set; }
        public int EventPhotoUrl { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}