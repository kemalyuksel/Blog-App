using Blog.Business.Tools.FacadeTool;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class ErrorController : ControllerBase
    {
        private readonly IFacade _facade;

        public ErrorController(IFacade facade)
        {
            _facade = facade;
        }

        [HttpGet]
        public IActionResult Error()
        {
            var errorInfo = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            _facade.CustomLogger.LogError($"\n\nHatanın Oluştuğu Yer :{errorInfo.Path}\n\n" +
                $"Hata Mesajı : {errorInfo.Error.Message} \n\n Stack Trace : {errorInfo.Error.StackTrace}\n\n");

            return Problem(detail: "an error occurred while processing your request");
        }

    }
}