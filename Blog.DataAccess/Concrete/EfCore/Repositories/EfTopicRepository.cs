using Blog.DataAccess.Abstract;
using Blog.DataAccess.Concrete.EfCore.Context;
using Blog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.Concrete.EfCore.Repositories
{
    public class EfTopicRepository : EfGenericRepository<Topic>, ITopicDal
    {
        private readonly BlogContext _context;
        public EfTopicRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Topic>> GetAllByCategoryIdAsync(int categoryId)
        {

            return await _context.Blogs.Join(_context.CategoryBlogs, b => b.Id, cb => cb.TopicId,
                (topic, categoryBlog) => new
                {
                    topic = topic,
                    categoryBlog = categoryBlog
                }).Where(x => x.categoryBlog.CategoryId == categoryId).Select(x => new Topic
                {
                    AppUser = x.topic.AppUser,
                    AppUserId = x.topic.AppUserId,
                    CategoryTopics = x.topic.CategoryTopics,
                    Comments = x.topic.Comments,
                    Description = x.topic.Description,
                    ShortDescription = x.topic.ShortDescription,
                    ImagePath = x.topic.ImagePath,
                    PostedTime = x.topic.PostedTime,
                    Id = x.topic.Id,
                    Title = x.topic.Title
                }).OrderByDescending(x => x.PostedTime).ToListAsync();
        }


        public async Task RemoveTopicAsync(Topic entity)
        {
            _context.Blogs.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetCategoriesAsync(int topicId)
        {
            return await _context.Categories.Join
                (_context.CategoryBlogs, c => c.Id, cb => cb.CategoryId, (category, categoryBlog)
                   => new
                   {
                       category = category,
                       categoryBlog = categoryBlog
                   }).Where(x => x.categoryBlog.TopicId == topicId).Select(x => new Category
                   {
                       Id = x.category.Id,
                       Name = x.category.Name
                   }).ToListAsync();
        }

        public async Task<List<Topic>> GetLastFiveAsync()
        {
            return await _context.Blogs.OrderByDescending(x => x.PostedTime ).Take(5).ToListAsync();

        }
    }
}
