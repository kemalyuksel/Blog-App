using AutoMapper;
using Blog.Dto.DTOs.CategoryDtos;
using Blog.Dto.DTOs.CommentDtos;
using Blog.Dto.DTOs.TopicDtos;
using Blog.Entities.Concrete;
using Blog.WebApi.Models;

namespace Blog.WebApi.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<TopicListDto, Topic>();
            CreateMap<Topic, TopicListDto>();

            CreateMap<TopicUpdateModel, Topic>();
            CreateMap<Topic, TopicUpdateModel>();

            CreateMap<TopicAddModel, Topic>();
            CreateMap<Topic, TopicAddModel>();


            CreateMap<CategoryAddDto, Category>();
            CreateMap<Category, CategoryAddDto>();

            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryUpdateDto>();

            CreateMap<CategoryListDto, Category>();
            CreateMap<Category, CategoryListDto>();

            CreateMap<CommentListDto, Comment>();
            CreateMap<Comment, CommentListDto>();

            CreateMap<CommentAddDto, Comment>();
            CreateMap<Comment, CommentAddDto>();

            CreateMap<CommentUpdateDto, Comment>();
            CreateMap<Comment, CommentUpdateDto>();




        }

    }
}
