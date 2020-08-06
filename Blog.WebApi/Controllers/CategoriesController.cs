using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Business.Abstract;
using Blog.Dto.DTOs.CategoryDtos;
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
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;


        public CategoriesController(IMapper mapper, ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            return Ok(_mapper.Map<List<CategoryListDto>>
                (await _categoryService.GetAllSortedByIdAsync()));
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(ValidId<Category>))]
        public async Task<IActionResult> GetById(int id)
        {

            return Ok(_mapper.Map<CategoryListDto>
                (await _categoryService.FindByIdAsync(id)));
        }

        [HttpPost]
        [Authorize]
        [ValidModel]
        public async Task<IActionResult> Create(CategoryAddDto categoryAddDto)
        {
            await _categoryService.AddAsync(_mapper.Map<Category>(categoryAddDto));
            return Created("", categoryAddDto);
        }

        [HttpPut("{id}")]
        [Authorize]
        [ValidModel]
        [ServiceFilter(typeof(ValidId<Category>))]
        public async Task<IActionResult> Update(int id,CategoryUpdateDto categoryUpdateDto)
        {
            if (id != categoryUpdateDto.Id)
            {
                return BadRequest("Invalid Id");
            }

            var updatedModel = await _categoryService.FindByIdAsync(categoryUpdateDto.Id);

            updatedModel.Id = categoryUpdateDto.Id;
            updatedModel.Name = categoryUpdateDto.Name;


            await _categoryService.UpdateAsync(updatedModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ServiceFilter(typeof(ValidId<Category>))]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.RemoveAsync(await _categoryService.FindByIdAsync(id));
            return NoContent();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetWithTopicsCount()
        {

            var categories = await _categoryService.GetAllWithCategoryTopicsAsync();
            List<CategoryWithTopicsCountDto> listCategory = new List<CategoryWithTopicsCountDto>();

            foreach (var item in categories)
            {
                CategoryWithTopicsCountDto dto = new CategoryWithTopicsCountDto();
                dto.CategoryName = item.Name;
                dto.CategoryId = item.Id;
                dto.TopicsCount = item.CategoryTopics.Count;

                listCategory.Add(dto);
            }

            return Ok(listCategory);

        }


    }
}