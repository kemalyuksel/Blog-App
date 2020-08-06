using Blog.Dto.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Dto.DTOs.CommentDtos
{
    public class CommentAddDto : IDto
    {
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public DateTime PostedTime { get; set; }
        public int? ParentCommentId { get; set; }
        public bool IsApproved { get; set; }
        public int TopicId { get; set; }
    }
}
