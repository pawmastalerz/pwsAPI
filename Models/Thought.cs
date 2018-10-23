namespace pwsAPI.Models
{
    public class Thought
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public short Visible { get; set; }
        public string ThoughtPhotoUrl { get; set; }
    }
}