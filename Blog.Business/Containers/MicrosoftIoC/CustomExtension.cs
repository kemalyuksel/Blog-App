using Blog.Business.Abstract;
using Blog.Business.Concrete;
using Blog.Business.Tools.FacadeTool;
using Blog.Business.Tools.JWTTool;
using Blog.Business.Tools.LogTool;
using Blog.Business.ValidationRules.FluentValidation;
using Blog.DataAccess.Abstract;
using Blog.DataAccess.Concrete.EfCore.Context;
using Blog.DataAccess.Concrete.EfCore.Repositories;
using Blog.Dto.DTOs.AppUserDtos;
using Blog.Dto.DTOs.CategoryDtos;
using Blog.Dto.DTOs.CategoryTopicDtos;
using Blog.Dto.DTOs.CommentDtos;
using Blog.Entities.Concrete;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Business.Containers.MicrosoftIoC
{
    public static class CustomExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BlogContext>(x =>
            {
                x.UseSqlServer(configuration.GetConnectionString("Local"), conf =>
                {
                    conf.MigrationsAssembly("Blog.WebApi");
                });
            });

            services.AddScoped(typeof(IGenericDal<>), typeof(EfGenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));

            services.AddScoped<ITopicService, TopicManager>();
            services.AddScoped<ITopicDal, EfTopicRepository>();

            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDal, EfCategoryRepository>();

            services.AddScoped<IAppUserService, AppUserManager>();
            services.AddScoped<IAppUserDal, EfAppUserRepository>();

            services.AddScoped<ICommentService, CommentManager>();
            services.AddScoped<ICommentDal, EfCommentRepository>();

            services.AddScoped<ICustomLogger, NLogAdapter>();
            services.AddScoped<IJwtService, JwtManager>();
            services.AddScoped<IFacade, Facade>();

            services.AddTransient<IValidator<AppUserLoginDto>, AppUserLoginValidator>();
            services.AddTransient<IValidator<CategoryAddDto>, CategoryAddValidator>();
            services.AddTransient<IValidator<CategoryTopicDto>, CategoryTopicValidator>();
            services.AddTransient<IValidator<CategoryUpdateDto>, CategoryUpdateValidator>();
            services.AddTransient<IValidator<CommentAddDto>, CommentAddValidator>();
        }

    }
}
