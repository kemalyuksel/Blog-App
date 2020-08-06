using AutoMapper;
using Blog.Business.Abstract;
using Blog.Business.Tools.FacadeTool;
using Blog.Dto.DTOs.CategoryDtos;
using Blog.Dto.DTOs.CategoryTopicDtos;
using Blog.Dto.DTOs.CommentDtos;
using Blog.Dto.DTOs.TopicDtos;
using Blog.Entities.Concrete;
using Blog.WebApi.CustomFilters;
using Blog.WebApi.Enums;
using Blog.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class TopicsController : BaseController
    {

        private readonly ITopicService _topicService;
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;
        private readonly IFacade _facade;

        public TopicsController(ITopicService topicService, IMapper mapper, ICommentService commentService,
            IFacade facade)
        {
            _facade = facade;
            _topicService = topicService;
            _mapper = mapper;
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            #region Cache
            //var topicList = _mapper.Map<List<TopicListDto>>(await _topicService.GetAllSortedByPostedTimeAsync());

            //if (_facade.MemoryCache.TryGetValue("topicList", out List<TopicListDto> list))
            //{
            //    return Ok(list);
            //}



            //_facade.MemoryCache.Set("topicList", topicList, new MemoryCacheEntryOptions()
            //{
            //    AbsoluteExpiration = DateTime.Now.AddDays(1),
            //    Priority = CacheItemPriority.Normal
            //});

            //return Ok(topicList); 
            #endregion

            return Ok(_mapper.Map<List<TopicListDto>>(await _topicService.GetAllSortedByPostedTimeAsync()));
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(ValidId<Topic>))]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<TopicListDto>(await _topicService.FindByIdAsync(id)));
        }

        [HttpPost]
        [Authorize]
        [ValidModel]
        public async Task<IActionResult> Create([FromForm]TopicAddModel topicAddModel)
        {
            var uploadModel = await UploadFileAsync(topicAddModel.Image, "image/jpeg");

            if (uploadModel.UploadState == UploadState.Success)
            {
                topicAddModel.ImagePath = uploadModel.NewName;
                await _topicService.AddAsync(_mapper.Map<Topic>(topicAddModel));
                return Created("", topicAddModel);
            }
            else if (uploadModel.UploadState == UploadState.NotExist)
            {
                await _topicService.AddAsync(_mapper.Map<Topic>(topicAddModel));
                return Created("", topicAddModel);
            }
            else
            {
                return BadRequest(uploadModel.ErrorMessage);
            }

        }

        [HttpPut("{id}")]
        [Authorize]
        [ValidModel]
        [ServiceFilter(typeof(ValidId<Topic>))]
        public async Task<IActionResult> Update(int id, [FromForm]TopicUpdateModel topicUpdateModel)
        {
            if (id != topicUpdateModel.Id)
                return BadRequest("Invalid Id");

            var uploadModel = await UploadFileAsync(topicUpdateModel.Image, "image/jpeg");

            if (uploadModel.UploadState == UploadState.Success)
            {
                var updatedTopic = await _topicService.FindByIdAsync(topicUpdateModel.Id);

                updatedTopic.ShortDescription = topicUpdateModel.ShortDescription;
                updatedTopic.Description = topicUpdateModel.Description;
                updatedTopic.Title = topicUpdateModel.Title;
                updatedTopic.ImagePath = uploadModel.NewName;
                await _topicService.UpdateAsync(updatedTopic);
                return NoContent();
            }
            else if (uploadModel.UploadState == UploadState.NotExist)
            {
                var updatedTopic = await _topicService.FindByIdAsync(topicUpdateModel.Id);

                updatedTopic.ShortDescription = topicUpdateModel.ShortDescription;
                updatedTopic.Description = topicUpdateModel.Description;
                updatedTopic.Title = topicUpdateModel.Title;

                await _topicService.UpdateAsync(updatedTopic);
                return NoContent();
            }
            else
            {
                return BadRequest(uploadModel.ErrorMessage);
            }

        }

        [HttpDelete("{id}")]
        [Authorize]
        [ServiceFilter(typeof(ValidId<Topic>))]
        public async Task<IActionResult> Delete(int id)
        {
            await _topicService.RemoveAsync(await _topicService.FindByIdAsync(id));
            return NoContent();
        }

        [HttpPost("[action]")]
        [ValidModel]
        public async Task<IActionResult> AddToCategory(CategoryTopicDto categoryTopicDto)
        {
            await _topicService.AddToCategoryAsync(categoryTopicDto);

            return Created("", categoryTopicDto);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> RemoveFromCategory([FromQuery] CategoryTopicDto categoryTopicDto)
        {
            await _topicService.RemoveFromCategoryAsync(categoryTopicDto);

            return NoContent();
        }


        [HttpGet("[action]/{id}")]
        [ServiceFilter(typeof(ValidId<Category>))]
        public async Task<IActionResult> GetAllByCategoryId(int id)
        {
            return Ok(await _topicService.GetAllByCategoryIdAsync(id));
        }

        [HttpGet("{id}/[action]")]
        public async Task<IActionResult> GetCategories(int id)
        {
            return Ok(_mapper.Map<List<CategoryListDto>>(await _topicService.GetCategoriesAsync(id)));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetLastFive()
        {

            #region Cache
            //if (_facade.MemoryCache.TryGetValue("lastFive", out List<TopicListDto> list))
            //{
            //    return Ok(list);
            //}

            //var lastFive = _mapper.Map<List<TopicListDto>>(await _topicService.GetLastFiveAsync());

            //_facade.MemoryCache.Set("lastFive", lastFive, new MemoryCacheEntryOptions()
            //{
            //    AbsoluteExpiration = DateTime.Now.AddDays(1),
            //    Priority = CacheItemPriority.Normal
            //});

            //return Ok(lastFive); 
            #endregion

            return Ok(_mapper.Map<List<TopicListDto>>(await _topicService.GetLastFiveAsync()));

        }


        [HttpGet("{id}/[action]")]
        public async Task<IActionResult> GetComments([FromRoute]int id, [FromQuery]int? parentCommentId)
        {

            return Ok(_mapper.Map<List<CommentListDto>>
                (await _commentService.GetAllWithSubCommentsAsync(id, parentCommentId)));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Search([FromQuery]string s)
        {
            return Ok(_mapper.Map<List<TopicListDto>>(await _topicService.SearchAsync(s)));
        }

        [HttpPost("[action]")]
        [ValidModel]
        public async Task<IActionResult> AddComment(CommentAddDto commentAddDto)
        {

            if (commentAddDto.AuthorName.Contains("Admin") || commentAddDto.AuthorName.Contains("admin"))
            {
                commentAddDto.AuthorName = "ÇakalKarlos";
            }

            commentAddDto.PostedTime = DateTime.Now;
            commentAddDto.IsApproved = false;
            await _commentService.AddAsync(_mapper.Map<Comment>(commentAddDto));

            return Created("", commentAddDto);

        }
    }
}