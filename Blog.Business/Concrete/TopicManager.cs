using Blog.Business.Abstract;
using Blog.DataAccess.Abstract;
using Blog.Dto.DTOs.CategoryTopicDtos;
using Blog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Concrete
{
    public class TopicManager : GenericManager<Topic>, ITopicService
    {
        private readonly IGenericDal<Topic> _genericDal;
        private readonly IGenericDal<CategoryTopic> _categoryTopicService;
        private readonly ITopicDal _topicDal;

        public TopicManager(IGenericDal<Topic> genericDal,
            IGenericDal<CategoryTopic> categoryTopicService,
            ITopicDal topicDal) : base(genericDal)
        {
            _genericDal = genericDal;
            _topicDal = topicDal;
            _categoryTopicService = categoryTopicService;
        }

        public async Task AddToCategoryAsync(CategoryTopicDto categoryTopicDto)
        {
            var controlAddCategory = await _categoryTopicService.GetAsync
              (x => x.CategoryId == categoryTopicDto.CategoryId &&
              x.TopicId == categoryTopicDto.TopicId);

            if (controlAddCategory == null)
            {
                await _categoryTopicService.AddAsync(new CategoryTopic
                {
                    TopicId = categoryTopicDto.TopicId,
                    CategoryId = categoryTopicDto.CategoryId
                });
            }


        }

        public async Task<List<Topic>> GetAllByCategoryIdAsync(int categoryId)
        {
            return await _topicDal.GetAllByCategoryIdAsync(categoryId);
        }

        public async Task<List<Topic>> GetAllSortedByPostedTimeAsync()
        {
            return await _genericDal.GetAllAsync(x => x.PostedTime);
        }



        public async Task<List<Category>> GetCategoriesAsync(int topicId)
        {
            return await _topicDal.GetCategoriesAsync(topicId);
        }

        public async Task<List<Topic>> GetLastFiveAsync()
        {
            return await _topicDal.GetLastFiveAsync();
        }

        public async Task RemoveFromCategoryAsync(CategoryTopicDto categoryTopicDto)
        {
            var deletedCategoryTopic = await _categoryTopicService.GetAsync
              (x => x.CategoryId == categoryTopicDto.CategoryId &&
              x.TopicId == categoryTopicDto.TopicId);

            if (deletedCategoryTopic != null)
            {
                await _categoryTopicService.RemoveAsync(deletedCategoryTopic);
            }

        }


        public async Task<List<Topic>> SearchAsync(string searchString)
        {
            return await _topicDal.GetAllAsync(x => x.Title.Contains(searchString) ||
            x.Description.Contains(searchString) || x.ShortDescription.Contains(searchString), x => x.PostedTime);
        }
    }
}
