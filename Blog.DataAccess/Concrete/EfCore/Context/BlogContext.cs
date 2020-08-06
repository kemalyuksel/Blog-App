using Blog.DataAccess.Mapping;
using Blog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DataAccess.Concrete.EfCore.Context
{
    public class BlogContext : DbContext
    {
        //private readonly IConfiguration _configuration;

        //public BlogContext(/*IConfiguration configuration, */DbContextOptions<BlogContext> options)
        //{
        //    _configuration = configuration;
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(_configuration.GetConnectionString("db1"));
        //    optionsBuilder.UseSqlServer("Server=KEMSTROISI;database=BlogDbNetCore;Integrated Security=true");

        //}

        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new CategoryTopicMap());
            modelBuilder.ApplyConfiguration(new TopicMap());
            modelBuilder.ApplyConfiguration(new CommentMap());
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Topic> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTopic> CategoryBlogs { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}
