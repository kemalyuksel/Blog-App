using Blog.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Entities.Concrete
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<CategoryTopic> CategoryTopics { get; set; }

    }
}
