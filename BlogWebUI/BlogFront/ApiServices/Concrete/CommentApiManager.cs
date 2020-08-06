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
    public class CommentApiManager : ICommentApiService
    {

        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CommentApiManager(HttpClient httpClient,
        IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:55377/api/comments/");
        }
        public async Task<List<CommentListModel>> GetAllAsync()
        {
            var responseMessage = await _httpClient.GetAsync("");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<CommentListModel>>(await
                responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<CommentApprovedModel> GetByIdAsync(int id)
        {
            var responseMessage = await _httpClient.GetAsync($"{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<CommentApprovedModel>(await
                responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task DeleteAsync(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session
            .GetString("token"));

            await _httpClient.DeleteAsync($"{id}");
        }

        public async Task UpdateAsync(CommentApprovedModel model)
        {
           MultipartFormDataContent formData = new MultipartFormDataContent();

            formData.Add(new StringContent(model.Id.ToString()), nameof(CommentApprovedModel.Id));
            formData.Add(new StringContent(model.Description.ToString()), nameof(CommentApprovedModel.Description));
            formData.Add(new StringContent(model.AuthorEmail.ToString()), nameof(CommentApprovedModel.AuthorEmail));
            formData.Add(new StringContent(model.AuthorName.ToString()), nameof(CommentApprovedModel.AuthorName));
            formData.Add(new StringContent(model.IsApproved.ToString()), nameof(CommentApprovedModel.IsApproved));

            _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));

            await _httpClient.PutAsync($"{model.Id}", formData);
        }


    }
}