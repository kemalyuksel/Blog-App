using System;
using System.Collections.Generic;

namespace BlogFront.Models
{
    public class CommentListModel
    {
        public int Id { get; set; }

        public string Description { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public DateTime PostedTime { get; set; }
        public bool IsApproved { get; set; } 

        public int? ParentCommentId { get; set; }

        public List<CommentListModel> SubComments { get; set; }

        public int TopicId { get; set; }
    }
}