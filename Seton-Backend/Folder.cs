using System;
using System.Collections.Generic;
namespace Seton_Backend
{
    public class Folder
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public List<FolderNote> FolderNotes { get; set; } = new();
    }
}