namespace Seton_Backend.Models.DTOs
{
    public class NoteCreateRequest
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
    }
}
