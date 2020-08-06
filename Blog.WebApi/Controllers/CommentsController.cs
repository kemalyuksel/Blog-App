using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Business.Abstract;
using Blog.Dto.DTOs.CommentDtos;
using Blog.Entities.Concrete;
using Blog.WebApi.CustomFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class CommentsController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;


        public CommentsController(IMapper mapper, ICommentService commentService)
        {
            _mapper = mapper;
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<CommentListDto>>
                (await _commentService.GetAllAsync()));
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(ValidId<Comment>))]
        public async Task<IActionResult> GetById(int id)
        {

            return Ok(_mapper.Map<CommentListDto>
                (await _commentService.FindByIdAsync(id)));
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ServiceFilter(typeof(ValidId<Comment>))]
        public async Task<IActionResult> Delete(int id)
        {
            await _commentService.RemoveAsync(await _commentService.FindByIdAsync(id));
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize]
        [ValidModel]
        [ServiceFilter(typeof(ValidId<Comment>))]
        public async Task<IActionResult> Update(int id, [FromForm]CommentUpdateDto commentUpdateDto)
        {
            if (id != commentUpdateDto.Id)
            {
                return BadRequest("Invalid Id");
            }

            var updatedComment = await _commentService.FindByIdAsync(id);

            updatedComment.Id = commentUpdateDto.Id;
            updatedComment.Description = commentUpdateDto.Description;
            updatedComment.AuthorEmail = commentUpdateDto.AuthorEmail;
            updatedComment.AuthorName = commentUpdateDto.AuthorName;
            updatedComment.IsApproved = commentUpdateDto.IsApproved;

            await _commentService.UpdateAsync(updatedComment);
            return NoContent();
        }

    }
}