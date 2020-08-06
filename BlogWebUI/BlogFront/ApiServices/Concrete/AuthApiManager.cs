using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlogFront.ApiServices.Abstract;
using BlogFront.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BlogFront.ApiServices.Concrete
{
    public class AuthApiManager : IAuthApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthApiManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _httpClient.BaseAddress = new Uri("http://localhost:55377/api/Auth/");
        }

        public async Task<bool> SignIn(AppUserLoginModel model)
        {
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync("SignIn", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                var accessToken = JsonConvert.DeserializeObject<AccessToken>(await responseMessage.Content.ReadAsStringAsync());

                _httpContextAccessor.HttpContext.Session.SetString("token", accessToken.Token);

                return true;
            }
            return false;
        }

    }
}