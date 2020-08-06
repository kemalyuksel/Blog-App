using Blog.Business.Abstract;
using Blog.DataAccess.Abstract;
using Blog.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Business.Concrete
{
    public class CategoryManager : GenericManager<Category>, ICategoryService
    {
        private readonly IGenericDal<Category> _genericDal;
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(IGenericDal<Category> genericDal,
            ICategoryDal categoryDal) : base(genericDal)
        {
            _genericDal = genericDal;
            _categoryDal = categoryDal;
        }

        public async Task<List<Category>> GetAllSortedByIdAsync()
        {
            return await _genericDal.GetAllAsync(x => x.Id);
        }

        public async Task<List<Category>> GetAllWithCategoryTopicsAsync()
        {
            return await _categoryDal.GetAllWithCategoryTopicsAsync();
        }
    }
}
