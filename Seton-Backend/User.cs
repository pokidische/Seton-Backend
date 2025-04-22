using System;
using System.Collections.Generic;
namespace Seton_Backend
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Note> Notes { get; set; } = new();
    }
}