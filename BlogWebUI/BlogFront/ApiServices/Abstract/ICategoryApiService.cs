using System.Collections.Generic;
using System.Threading.Tasks;
using BlogFront.Models;

namespace BlogFront.ApiServices.Abstract
{
    public interface ICategoryApiService
    {
        Task<List<CategoryListModel>> GetAllAsync();
        Task<List<CategoryWithTopicsCountDto>> GetAllWithTopicsCount();
        Task<CategoryListModel> GetByIdAsync(int id);
        Task AddAsync(CategoryAddModel model);
        Task UpdateAsync(CategoryUpdateModel model);
        Task DeleteAsync(int id);
    }
}