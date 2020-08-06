using Blog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.Abstract
{
    public interface ITopicDal : IGenericDal<Topic>
    {
        Task<List<Topic>> GetAllByCategoryIdAsync(int categoryId);
        Task<List<Category>> GetCategoriesAsync(int topicId);
        Task<List<Topic>> GetLastFiveAsync();
    }
}
