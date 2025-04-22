using System;
using System.Collections.Generic;
namespace Seton_Backend
{
    public class NoteTag
    {
        public int NoteId { get; set; }
        public Note Note { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}