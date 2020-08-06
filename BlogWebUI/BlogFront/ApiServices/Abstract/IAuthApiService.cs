using System.Threading.Tasks;
using BlogFront.Models;

namespace BlogFront.ApiServices.Abstract
{
    public interface IAuthApiService
    {
        Task<bool> SignIn(AppUserLoginModel model);
    }
}