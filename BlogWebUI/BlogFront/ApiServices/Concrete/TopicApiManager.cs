using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BlogFront.ApiServices.Abstract;
using BlogFront.Extensions;
using BlogFront.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BlogFront.ApiServices.Concrete
{
    public class TopicApiManager : ITopicApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TopicApiManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:55377/api/topics/");
        }
        public async Task<List<TopicListModel>> GetAllAsync()
        {

            var responseMessage = await _httpClient.GetAsync("");

            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<TopicListModel>>
                (await responseMessage.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task<TopicListModel> GetByIdAsync(int id)
        {
            var responseMessage = await _httpClient.GetAsync($"{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TopicListModel>(await
                responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<List<TopicListModel>> GetAllByCategoryIdAsync(int id)
        {
            var responseMessage = await _httpClient.GetAsync($"GetAllByCategoryId/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<TopicListModel>>(await
                responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task AddAsync(TopicAddModel model)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();

            if (model.Image != null)
            {
                var stream = new MemoryStream();
                await model.Image.CopyToAsync(stream);
                var bytes = stream.ToArray();
                ByteArrayContent byteContent = new ByteArrayContent(bytes);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);

                formData.Add(byteContent, nameof(TopicUpdateModel.Image), model.Image.FileName);
            }

            var user = _httpContextAccessor.HttpContext.Session.GetObject<AppUserViewModel>("activeUser");

            model.AppUserId = user.Id;

            formData.Add(new StringContent(model.AppUserId.ToString()), nameof(TopicAddModel.AppUserId));
            formData.Add(new StringContent(model.Description.ToString()), nameof(TopicAddModel.Description));
            formData.Add(new StringContent(model.ShortDescription.ToString()), nameof(TopicAddModel.ShortDescription));
            formData.Add(new StringContent(model.Title.ToString()), nameof(TopicAddModel.Title));

            _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));

            await _httpClient.PostAsync("", formData);
        }

        public async Task UpdateAsync(TopicUpdateModel model)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();

            if (model.Image != null)
            {
                var stream = new MemoryStream();
                await model.Image.CopyToAsync(stream);
                var bytes = stream.ToArray();
                ByteArrayContent byteContent = new ByteArrayContent(bytes);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);

                formData.Add(byteContent, nameof(TopicUpdateModel.Image), model.Image.FileName);
            }

            var user = _httpContextAccessor.HttpContext.Session.GetObject<AppUserViewModel>("activeUser");

            model.AppUserId = user.Id;

            formData.Add(new StringContent(model.Id.ToString()), nameof(TopicUpdateModel.Id));
            formData.Add(new StringContent(model.AppUserId.ToString()), nameof(TopicUpdateModel.AppUserId));
            formData.Add(new StringContent(model.Description.ToString()), nameof(TopicUpdateModel.Description));
            formData.Add(new StringContent(model.ShortDescription.ToString()), nameof(TopicUpdateModel.ShortDescription));
            formData.Add(new StringContent(model.Title.ToString()), nameof(TopicUpdateModel.Title));

            _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));

            await _httpClient.PutAsync($"{model.Id}", formData);
        }

        public async Task DeleteAsync(int id)
        {

            _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session
            .GetString("token"));

            await _httpClient.DeleteAsync($"{id}");
        }

        public async Task<List<CommentListModel>> GetCommentsAsync(int topicId, int? parentCommentId)
        {
            var responseMessage = await _httpClient.GetAsync
            ($"{topicId}/GetComments?parentCommentId?{parentCommentId}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<CommentListModel>>
                (await responseMessage.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task AddToComment(CommentAddModel model)
        {
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            await _httpClient.PostAsync("AddComment", content);
        }


        public async Task<List<CategoryListModel>> GetCategoriesAsync(int topicId)
        {
            var responseMessage = await _httpClient.GetAsync($"{topicId}/GetCategories");

            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<CategoryListModel>>
                (await responseMessage.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task<List<TopicListModel>> GetLastFiveAsync()
        {
            var responseMessage = await _httpClient.GetAsync("GetLastFive");

            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<TopicListModel>>
                (await responseMessage.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task<List<TopicListModel>> Search(string s)
        {
            var responseMessage = await _httpClient.GetAsync($"Search?s={s}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<TopicListModel>>
                (await responseMessage.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task AddToCategoryAsync(CategoryTopicModel categoryTopicModel)
        {
            var jsonData = JsonConvert.SerializeObject(categoryTopicModel);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            await _httpClient.PostAsync("AddToCategory", content);
        }

        public async Task RemoveFromCategoryAsync(CategoryTopicModel model)
        {
            await _httpClient.DeleteAsync($"RemoveFromCategory?" +
            $"{nameof(CategoryTopicModel.CategoryId)}={model.CategoryId}" +
            $"&{nameof(CategoryTopicModel.TopicId)}={model.TopicId}");
        }


    }
}