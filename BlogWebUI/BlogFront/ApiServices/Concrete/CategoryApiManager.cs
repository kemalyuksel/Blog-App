using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BlogFront.ApiServices.Abstract;
using BlogFront.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BlogFront.ApiServices.Concrete
{
    public class CategoryApiManager : ICategoryApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CategoryApiManager(HttpClient httpClient,
        IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:55377/api/categories/");
        }
        public async Task<List<CategoryListModel>> GetAllAsync()
        {
            var responseMessage = await _httpClient.GetAsync("");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<CategoryListModel>>(await
                responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<List<CategoryWithTopicsCountDto>> GetAllWithTopicsCount()
        {
            var responseMessage = await _httpClient.GetAsync("GetWithTopicsCount");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<CategoryWithTopicsCountDto>>(await
                responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<CategoryListModel> GetByIdAsync(int id)
        {
            var responseMessage = await _httpClient.GetAsync($"{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<CategoryListModel>(await
                responseMessage.Content.ReadAsStringAsync());
            }
            return null;

        }

        public async Task AddAsync(CategoryAddModel model)
        {
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            _httpContextAccessor.HttpContext.Session.GetString("token"));

            await _httpClient.PostAsync("", content);
        }

        public async Task UpdateAsync(CategoryUpdateModel model)
        {
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            _httpContextAccessor.HttpContext.Session.GetString("token"));

            await _httpClient.PutAsync($"{model.Id}", content);
        }

        public async Task DeleteAsync(int id){
            
            _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session
            .GetString("token"));

           await _httpClient.DeleteAsync($"{id}");
        }
        
    }
}