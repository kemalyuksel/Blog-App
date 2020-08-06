using System.Threading.Tasks;

namespace BlogFront.ApiServices.Abstract
{
    public interface IImageApiService
    {
        Task<string> GetTopicImageByIdAsync(int id);
    }
}