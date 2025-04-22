namespace Seton_Backend
{
    public class VoiceNote
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public Note Note { get; set; }
        public string AudioPath { get; set; }
        public string Transcription { get; set; }
    }
}