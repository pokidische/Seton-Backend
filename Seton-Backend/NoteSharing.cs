using System;
using System.Collections.Generic;
namespace Seton_Backend
{
    public class NoteSharing
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public Note Note { get; set; }
        public int SharedWithUserId { get; set; }
        public User SharedWithUser { get; set; }
        public bool CanEdit { get; set; }
    }
}