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
    public class EfCategoryRepository : EfGenericRepository<Category>, ICategoryDal
    {

        private readonly BlogContext _context;

        public EfCategoryRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllWithCategoryTopicsAsync()
        {
            return await _context.Categories.OrderByDescending(x => x.Id).
                Include(x => x.CategoryTopics).ToListAsync();
        }
    }
}
