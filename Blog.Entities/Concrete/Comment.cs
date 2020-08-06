using Blog.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Entities.Concrete
{
    public class Comment : IEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public DateTime PostedTime { get; set; } = DateTime.Now;
        public bool IsApproved { get; set; }

        public int? ParentCommentId { get; set; }
        public Comment ParentComment { get; set; }

        public List<Comment> SubComments { get; set; }

        public int TopicId { get; set; }
        public Topic Topic { get; set; }

    }
}
