using System;
using System.Collections.Generic;
namespace Seton_Backend
{
    public class NoteVersion
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public Note Note { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}