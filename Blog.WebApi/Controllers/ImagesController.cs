using System.Threading.Tasks;
using Blog.Business.Abstract;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class ImagesController : ControllerBase
    {
        private readonly ITopicService _topicService;

        public ImagesController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetTopicImageById(int id)
        {

            var topic = await _topicService.FindByIdAsync(id);

            if (string.IsNullOrWhiteSpace(topic.ImagePath))
                return NotFound("Image is not exist");

            return File($"/img/{topic.ImagePath}","image/jpeg");
        }


    }
}