using Blog.Business.Abstract;
using Blog.DataAccess.Abstract;
using Blog.Dto.DTOs.AppUserDtos;
using Blog.Entities.Concrete;
using System.Threading.Tasks;

namespace Blog.Business.Concrete
{
    public class AppUserManager : GenericManager<AppUser>, IAppUserService
    {
        private readonly IGenericDal<AppUser> _genericDal;

        public AppUserManager(IGenericDal<AppUser> genericDal) : base(genericDal)
        {
            _genericDal = genericDal;
        }

        public async Task<AppUser> CheckUserAsync(AppUserLoginDto appUserLoginDto)
        {

            return await _genericDal.GetAsync(x => x.UserName == appUserLoginDto.UserName &&
            x.Password == appUserLoginDto.Password);
        }

        public async Task<AppUser> FindByNameAsync(string userName)
        {
            return await _genericDal.GetAsync(x => x.UserName == userName);

            
        }
    }
}
