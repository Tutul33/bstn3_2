using System;
using System.Collections.Generic;

namespace feedback.Models
{
    public partial class OpinionLog
    {
        public int OpinionLogId { get; set; }
        public int? CommentId { get; set; }
        public bool? IsLike { get; set; }
        public int? UserId { get; set; }
    }
}
