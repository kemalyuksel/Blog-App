namespace BlogFront.Models
{
    public class CategoryWithTopicsCountDto
    {
        public int TopicsCount { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}