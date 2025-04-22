using System;
using System.Collections.Generic;
namespace Seton_Backend
{
    public class Note
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Type { get; set; } // 'text', 'todo', 'drawing', 'voice'
        public bool IsPinned { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool Encrypted { get; set; }
        public List<TodoItem> TodoItems { get; set; } = new();
        public List<NoteTag> NoteTags { get; set; } = new();
        public List<NoteSharing> NoteSharings { get; set; } = new();
    }
}