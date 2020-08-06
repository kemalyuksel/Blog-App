using System.Threading.Tasks;
using Blog.Business.Abstract;
using Blog.Business.Tools.JWTTool;
using Blog.Dto.DTOs.AppUserDtos;
using Blog.WebApi.CustomFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class AuthController : ControllerBase
    {

        private readonly IAppUserService _appUserService;
        private readonly IJwtService _jwtService;

        public AuthController(IAppUserService appUserService, IJwtService jwtService)
        {
            _appUserService = appUserService;
            _jwtService = jwtService;
        }

        [HttpPost("[action]")]
        [ValidModel]
        public async Task<IActionResult> SignIn(AppUserLoginDto appUserLoginDto)
        {

            var user = await _appUserService.CheckUserAsync(appUserLoginDto);

            if (user != null)
            {
                return Created("", _jwtService.GenerateJwt(user));
            }

            return BadRequest("Invalid username or password");
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> ActiveUser()
        {
            var user = await _appUserService.FindByNameAsync(User.Identity.Name);

            return Ok(new AppUserDto { Id = user.Id, Name = user.Name, SurName = user.Surname });
        }

    }
}