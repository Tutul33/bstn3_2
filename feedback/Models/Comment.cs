using System;
using System.Collections.Generic;

namespace feedback.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public int PostId { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? CLike { get; set; }
        public int? CDislike { get; set; }
        public int? UserId { get; set; }

        public User User { get; set; }
    }
}
