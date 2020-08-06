using System.Collections.Generic;
using System.Threading.Tasks;
using BlogFront.Models;

namespace BlogFront.ApiServices.Abstract
{
    public interface ITopicApiService
    {
        Task<List<TopicListModel>> GetAllAsync();
        Task<TopicListModel> GetByIdAsync(int id);
        Task<List<TopicListModel>> GetAllByCategoryIdAsync(int id);
        Task AddAsync(TopicAddModel model);
        Task UpdateAsync(TopicUpdateModel model);
        Task DeleteAsync(int id);
        Task<List<CommentListModel>> GetCommentsAsync(int topicId, int? parentCommentId);
        Task AddToComment(CommentAddModel model);
        Task<List<CategoryListModel>> GetCategoriesAsync(int topicId);
        Task<List<TopicListModel>> GetLastFiveAsync();
        Task<List<TopicListModel>> Search(string s);
        Task AddToCategoryAsync(CategoryTopicModel categoryTopicModel);
        Task RemoveFromCategoryAsync(CategoryTopicModel categoryTopicModel);

    }
}