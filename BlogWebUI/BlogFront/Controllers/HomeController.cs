using System.Threading.Tasks;
using BlogFront.ApiServices.Abstract;
using BlogFront.Models;
using Microsoft.AspNetCore.Mvc;

namespace UdemyBlogFront.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITopicApiService _topicApiService;
        public HomeController(ITopicApiService topicApiService)
        {
            _topicApiService = topicApiService;
        }
        public async Task<IActionResult> Index(int? categoryId, string s)
        {

            if (categoryId.HasValue)
            {
                ViewBag.ActiveCategory = categoryId;
                return View(await _topicApiService.GetAllByCategoryIdAsync((int)categoryId));
            }
            if (!string.IsNullOrWhiteSpace(s))
            {
                ViewBag.SearchString = s;
                return View(await _topicApiService.Search(s));
            }

            return View(await _topicApiService.GetAllAsync());
        }

        public async Task<IActionResult> TopicDetail(int id)
        {
            var topic = await _topicApiService.GetByIdAsync(id);

            if (topic != null)
            {
                ViewBag.Comments = await _topicApiService.GetCommentsAsync(id, null);

                return View(await _topicApiService.GetByIdAsync(id));
            }

            TempData["notFound"] = "Blog bulunamadı";
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> AddComment(CommentAddModel model)
        {
            await _topicApiService.AddToComment(model);
            return RedirectToAction("TopicDetail", new { id = model.TopicId });
        }

    }
}