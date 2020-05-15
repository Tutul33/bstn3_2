using System;
using System.Collections.Generic;

namespace feedback.Models
{
    public partial class User
    {
        public User()
        {
            Comment = new HashSet<Comment>();
            Post = new HashSet<Post>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string Password { get; set; }

        public ICollection<Comment> Comment { get; set; }
        public ICollection<Post> Post { get; set; }
    }
}
