namespace pwsAPI.Models
{
    public class Thought
    {
        public int Id { get; set; }
        public string Quote { get; set; }
        public string Author { get; set; }
        public short Accepted { get; set; }
        public string ThoughtPhotoUrl { get; set; }
    }
}