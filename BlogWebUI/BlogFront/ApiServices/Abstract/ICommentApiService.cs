using System.Collections.Generic;
using System.Threading.Tasks;
using BlogFront.Models;

namespace BlogFront.ApiServices.Abstract
{
    public interface ICommentApiService
    {
        Task<List<CommentListModel>> GetAllAsync();
        Task UpdateAsync(CommentApprovedModel model);
        Task<CommentApprovedModel> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}