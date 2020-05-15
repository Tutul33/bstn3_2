using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace feedback.ViewModels
{
    public class vmFeedback
    {
        public int PostId { get; set; }
        public string PostText { get; set; }
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public DateTime? CreationTime { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
    }
    public  class vmUser
    {
        public vmUser()
        {
            Post = new HashSet<vmPost>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }

        public ICollection<vmPost> Post { get; set; }
    }
    public  class vmPost
    {
        public vmPost()
        {
            Comment = new HashSet<vmComment>();
        }

        public int PostId { get; set; }
        public string PostText { get; set; }
        public DateTime? CreationTime { get; set; }
        public int UserId { get; set; }
        public ICollection<vmComment> Comment { get; set; }
    }
    public  class vmComment
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public int PostId { get; set; }
        public DateTime? CreationTime { get; set; }
    }
}
