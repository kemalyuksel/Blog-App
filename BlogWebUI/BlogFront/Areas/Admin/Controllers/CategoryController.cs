using System.Threading.Tasks;
using BlogFront.ApiServices.Abstract;
using BlogFront.Filters;
using BlogFront.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogFront.Areas.Admin.Controllers
{
    [JwtAuthorize]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryApiService _categoryApiService;

        public CategoryController(ICategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }

        public async Task<IActionResult> Index()
        {
            TempData["active"] = "category";
            return View(await _categoryApiService.GetAllAsync());
        }

        public IActionResult AddCategory()
        {
            TempData["active"] = "category";
            return View(new CategoryAddModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryAddModel model)
        {
            if (ModelState.IsValid)
            {
                await _categoryApiService.AddAsync(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> UpdateCategory(int id)
        {
            TempData["active"] = "category";
            var categoryList = await _categoryApiService.GetByIdAsync(id);

            return View(new CategoryUpdateModel
            {
                Id = categoryList.Id,
                Name = categoryList.Name
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                await _categoryApiService.UpdateAsync(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryApiService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public IActionResult SignOut()
        {
            HttpContext.Session.Remove("token");

            return RedirectToAction("Index", "Home", new { area = "" });

        }

    }
}