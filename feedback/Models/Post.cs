using System;
using System.Collections.Generic;

namespace feedback.Models
{
    public partial class Post
    {
        public int PostId { get; set; }
        public string PostText { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? UserId { get; set; }

        public User User { get; set; }
    }
}
