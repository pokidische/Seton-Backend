using System;
using System.Collections.Generic;
namespace Seton_Backend
{
    public class TodoItem
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public Note Note { get; set; }
        public string Task { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? Reminder { get; set; }
    }
}