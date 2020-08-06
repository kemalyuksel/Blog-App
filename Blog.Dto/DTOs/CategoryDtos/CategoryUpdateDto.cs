using Blog.Dto.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Dto.DTOs.CategoryDtos
{
    public class CategoryUpdateDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
