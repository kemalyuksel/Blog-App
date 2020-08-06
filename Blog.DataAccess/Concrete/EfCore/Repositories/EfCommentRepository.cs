using Blog.DataAccess.Abstract;
using Blog.DataAccess.Concrete.EfCore.Context;
using Blog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.Concrete.EfCore.Repositories
{
    public class EfCommentRepository : EfGenericRepository<Comment>, ICommentDal
    {
        private readonly BlogContext _context;

        public EfCommentRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllWithSubCommentsAsync(int topicId, int? parentId)
        {
            List<Comment> result = new List<Comment>();
            await GetComments(topicId, parentId, result);
            return result;
        }

        private async Task GetComments(int topicId, int? parentId, List<Comment> result)
        {
            var comments = await _context.Comments.Where(x => x.TopicId == topicId && x.IsApproved 
            &&x.ParentCommentId == parentId).OrderBy(x => x.PostedTime).ToListAsync();

            if (comments.Count > 0)
            {
                foreach (var item in comments)
                {
                    if (item.SubComments == null)
                        item.SubComments = new List<Comment>();

                    await GetComments(item.TopicId, item.Id, item.SubComments);

                    if (!result.Contains(item))
                    {
                        result.Add(item);
                    }
                }
            }
        }
    }
}
