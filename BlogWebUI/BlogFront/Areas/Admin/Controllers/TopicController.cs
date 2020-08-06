using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogFront.ApiServices.Abstract;
using BlogFront.Filters;
using BlogFront.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogFront.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class TopicController : Controller
    {
        private readonly ITopicApiService _topicApiService;
        public TopicController(ITopicApiService topicApiService)
        {
            _topicApiService = topicApiService;
        }

        public async Task<IActionResult> Index()
        {
            TempData["active"] = "topic";
            return View(await _topicApiService.GetAllAsync());
        }

        [HttpGet]
        public IActionResult AddTopic()
        {
            TempData["active"] = "topic";
            return View(new TopicAddModel());
        }   

        [HttpPost]
        public async Task<IActionResult> AddTopic(TopicAddModel model)
        {
            TempData["active"] = "topic";
            if (ModelState.IsValid)
            {
                await _topicApiService.AddAsync(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTopic(int id)
        {
            TempData["active"] = "topic";
            var topicList = await _topicApiService.GetByIdAsync(id);

            return View(new TopicUpdateModel
            {
                Id = topicList.Id,
                Title = topicList.Title,
                Description = topicList.Description,
                ShortDescription = topicList.ShortDescription
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTopic(TopicUpdateModel model)
        {
            TempData["active"] = "topic";
            if (ModelState.IsValid)
            {
                await _topicApiService.UpdateAsync(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }


        public async Task<IActionResult> DeleteTopic(int id)
        {
            TempData["active"] = "topic";
            await _topicApiService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AssignCategory(int id,
        [FromServices]ICategoryApiService categoryApiService)
        {
            TempData["active"] = "topic";
            var categories = await categoryApiService.GetAllAsync();
            var topicCategories = (await _topicApiService.GetCategoriesAsync(id));

            TempData["topicId"] = id;

            List<AssignCategoryModel> list = new List<AssignCategoryModel>();

            foreach (var item in categories)
            {
                AssignCategoryModel model = new AssignCategoryModel();
                model.CategoryId = item.Id;
                model.CategoryName = item.Name;
                model.Exist = topicCategories.Contains(item);

                list.Add(model);
            }

            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> AssignCategory(List<AssignCategoryModel> list)
        {
            TempData["active"] = "topic";
            int id = (int)TempData["topicId"];

            foreach (var item in list)
            {
                if (item.Exist)
                {
                    CategoryTopicModel model = new CategoryTopicModel();
                    model.TopicId = id;
                    model.CategoryId = item.CategoryId;

                    await _topicApiService.AddToCategoryAsync(model);
                }
                else
                {
                    CategoryTopicModel model = new CategoryTopicModel();
                    model.TopicId = id;
                    model.CategoryId = item.CategoryId;

                    await _topicApiService.RemoveFromCategoryAsync(model);
                }
            }

            return RedirectToAction("Index");

        }



    }
}