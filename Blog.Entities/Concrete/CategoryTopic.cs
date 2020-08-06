using Blog.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Entities.Concrete
{
    public class CategoryTopic : IEntity
    {
        public int Id { get; set; }

        public int TopicId { get; set; }
        public Topic Topic { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }


    }
}
