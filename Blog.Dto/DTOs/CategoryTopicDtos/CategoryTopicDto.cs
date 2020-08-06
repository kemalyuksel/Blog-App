using Blog.Dto.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Dto.DTOs.CategoryTopicDtos
{
    public class CategoryTopicDto : IDto
    {
        public int CategoryId { get; set; }
        public int TopicId { get; set; }
    }
}
