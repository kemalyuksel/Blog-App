using Blog.Dto.DTOs.CategoryTopicDtos;
using Blog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Abstract
{
    public interface ITopicService : IGenericService<Topic>
    {
        Task<List<Topic>> GetAllSortedByPostedTimeAsync();
        Task AddToCategoryAsync(CategoryTopicDto categoryTopicDto);
        Task RemoveFromCategoryAsync(CategoryTopicDto categoryTopicDto);
        Task<List<Topic>> GetAllByCategoryIdAsync(int categoryId);
        Task<List<Category>> GetCategoriesAsync(int topicId);
        Task<List<Topic>> GetLastFiveAsync();
        Task<List<Topic>> SearchAsync(string searchString);
    }
}
