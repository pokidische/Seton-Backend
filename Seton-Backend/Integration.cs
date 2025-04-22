using System;
using System.Collections.Generic;
namespace Seton_Backend
{
    public class Integration
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Service { get; set; }
        public string AccessToken { get; set; }
    }
}