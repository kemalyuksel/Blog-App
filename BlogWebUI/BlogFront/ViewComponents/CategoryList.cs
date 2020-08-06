using BlogFront.ApiServices.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogFront.ViewComponents{
    public class CategoryList:ViewComponent{
        private readonly ICategoryApiService _categoryApiService;
        public CategoryList(ICategoryApiService categoryApiService)
        {
            _categoryApiService=categoryApiService;
        }
        public IViewComponentResult Invoke(){
            return View(_categoryApiService.GetAllWithTopicsCount().Result);
        }
    }
}