using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Dto.DTOs.CommentDtos
{
    public class CommentUpdateDto
    {
        public int Id { get; set; }

        public string Description { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public bool IsApproved { get; set; }
    }
}
