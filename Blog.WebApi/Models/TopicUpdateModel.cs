using Microsoft.AspNetCore.Http;

namespace Blog.WebApi.Models
{
    public class TopicUpdateModel
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }
    }
}
