using Blog.DataAccess.Abstract;
using Blog.DataAccess.Concrete.EfCore.Context;
using Blog.Entities.Concrete;

namespace Blog.DataAccess.Concrete.EfCore.Repositories
{
    public class EfAppUserRepository : EfGenericRepository<AppUser>, IAppUserDal
    {

        public EfAppUserRepository(BlogContext context):base(context)
        {

        }

    }
}
