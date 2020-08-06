using Blog.Dto.Abstract;
using Blog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Dto.DTOs.CategoryDtos
{
    public class CategoryWithTopicsCountDto : IDto
    {
        public int TopicsCount { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
