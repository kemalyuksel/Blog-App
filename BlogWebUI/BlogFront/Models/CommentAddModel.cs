using System;

namespace BlogFront.Models
{
    public class CommentAddModel
    {
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public DateTime PostedTime { get; set; }
        public int? ParentCommentId { get; set; }
        public int TopicId { get; set; }
    }
}