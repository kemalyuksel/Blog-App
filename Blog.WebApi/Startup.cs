using System;
using System.Text;
using AutoMapper;
using Blog.Business.Containers.MicrosoftIoC;
using Blog.Business.StringInfos;
using Blog.WebApi.CustomFilters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Blog.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(x =>
            {
                x.AddPolicy("global", cors =>
                 {
                     cors.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                 });
            });

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("doc", new OpenApiInfo
                {
                    Title = "BlogApi",
                    Description = "Blog Api Document",
                    Contact = new OpenApiContact
                    {
                        Email = "kemalyksell98@gmail.com",
                        Name = "Kemal YÜKSEL"
                    }
                });

                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Description = "Bearer {token}"
                });

            });

            services.Configure<JwtInfo>(Configuration.GetSection("JWTInfo"));

            var jwtInfo = Configuration.GetSection("JWTInfo").Get<JwtInfo>();

            services.AddAutoMapper(typeof(Startup));
            services.AddDependencies(Configuration);
            services.AddScoped(typeof(ValidId<>));
            services.AddMemoryCache();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtInfo.Issuer,
                        ValidAudience = jwtInfo.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(jwtInfo.SecurityKey)),
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ClockSkew = TimeSpan.Zero

                    };
                });

            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }).AddFluentValidation();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseExceptionHandler("/api/Error");
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("global");

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/doc/swagger.json", "Blog Api");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
