using System;
using System.Collections.Generic;
namespace Seton_Backend
{
    public class FolderNote
    {
        public int FolderId { get; set; }
        public Folder Folder { get; set; }
        public int NoteId { get; set; }
        public Note Note { get; set; }
    }
}