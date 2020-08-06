using System.Threading.Tasks;
using BlogFront.ApiServices.Abstract;
using BlogFront.Filters;
using BlogFront.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogFront.Areas.Admin.Controllers
{
    [JwtAuthorize]
    [Area("Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentApiService _commentService;

        public CommentController(ICommentApiService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            TempData["active"] = "comment";
            return View(await _commentService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> UpdateComment(int id)
        {
            TempData["active"] = "comment";

            var comment = await _commentService.GetByIdAsync(id);

            return View(new CommentApprovedModel
            {
                Id = comment.Id,
                Description = comment.Description,
                AuthorEmail = comment.AuthorEmail,
                AuthorName = comment.AuthorName,
                IsApproved = comment.IsApproved
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateComment(CommentApprovedModel model)
        {
            if (ModelState.IsValid)
            {
                await _commentService.UpdateAsync(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }


        public async Task<IActionResult> DeleteComment(int id)
        {
            TempData["active"] = "comment";
            await _commentService.DeleteAsync(id);
            return RedirectToAction("Index");
        }


    }
}