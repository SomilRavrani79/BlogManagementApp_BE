using System;

namespace BlogManagementApp.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime DateCreated { get; set; }
        public string Text { get; set; }
    }
}
