using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlogFront.ApiServices.Abstract;

namespace BlogFront.ApiServices.Concrete
{
    public class ImageApiManager : IImageApiService
    {
        private readonly HttpClient _httpClient;
        public ImageApiManager(HttpClient httpClient)
        {
            _httpClient=httpClient;
            _httpClient.BaseAddress=new Uri("http://localhost:55377/api/images/");
        }
       
          public async Task<string> GetTopicImageByIdAsync(int id)
        {

            var responseMessage = await _httpClient.GetAsync($"GetTopicImageById/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var bytes = await responseMessage.Content.ReadAsByteArrayAsync();

                return $"data:image/jpeg;base64,{Convert.ToBase64String(bytes)}";
            }

            return null;
        }


    }
}