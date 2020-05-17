using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace feedback.ViewModels
{
   
    public class vmPostList
    {
        [JsonProperty("postId")]
        public int PostId { get; set; }
        [JsonProperty("userId")]
        public int? UserId { get; set; }

        [JsonProperty("postText")]
        public string PostText { get; set; }

        [JsonProperty("userType")]
        public string UserType { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("creationTime")]
        public DateTime? CreationTime { get; set; }

        [JsonProperty("commentList")]
        public List<CommentList> CommentList { get; set; }

        [JsonProperty("recordsTotal")]
        public int RecordsTotal { get; set; }
    }

    public class CommentList
    {
        [JsonProperty("commentId")]
        public int CommentId { get; set; }

        [JsonProperty("commentText")]
        public string CommentText { get; set; }

        [JsonProperty("creationTime")]
        public DateTime? CreationTime { get; set; }

        [JsonProperty("cLike")]
        public int? CLike { get; set; }

        [JsonProperty("cDislike")]
        public int? CDislike { get; set; }

        [JsonProperty("postId")]
        public int PostId { get; set; }

        [JsonProperty("userId")]
        public int? UserId { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("userType")]
        public string UserType { get; set; }
    }
}
