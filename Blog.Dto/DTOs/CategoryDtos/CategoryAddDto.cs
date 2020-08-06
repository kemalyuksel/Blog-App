using Blog.Dto.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Dto.DTOs.CategoryDtos
{
    public class CategoryAddDto : IDto
    {
        public string Name { get; set; }
    }
}
