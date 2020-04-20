using System;
using AutoMapper;
using LSG.GenericCrud.DataFillers;
using LSG.GenericCrud.Dto.Helpers;
using LSG.GenericCrud.Dto.Services;
using LSG.GenericCrud.Helpers;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Samples.Models;
using LSG.GenericCrud.Samples.Models.DTOs;
using LSG.GenericCrud.Samples.Models.Entities;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LSG.GenericCrud.Samples
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
            services.AddAutoMapper(typeof(Startup));

            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddDbContext<SampleContext>(opt => opt.UseSqlServer("server=localhost;user id=sa;password=Sapassword1!;Initial Catalog=LSG.GenericCrud.Samples"));
            services.AddTransient<IDbContext, SampleContext>();
            services.AddTransient<IUserInfoRepository, UserInfoRepository>();
            services.AddTransient<IEntityDataFiller, CreatedFiller>();
            services.AddTransient<IEntityDataFiller, ModifiedFiller>();


            // to inject a specific layer for all type of object that implemend IEntity<T>
            // services.AddScoped(typeof(ICrudService<Guid, Share>), typeof(CustomImplementedCrudService<Guid, Share>));

            services.AddScoped(typeof(ICrudService<Guid, ItemDto>), typeof(CrudServiceBase<Guid, ItemDto, Item>));
            services.AddScoped(typeof(ICrudService<Guid, UserDto>), typeof(CrudServiceBase<Guid, UserDto, User>));
            services.AddScoped(typeof(ICrudService<Guid, BlogPostDto>), typeof(CrudServiceBase<Guid, BlogPostDto, BlogPost>));

            services.AddCrud();
            services.AddCrudDto();
            // services.AddScoped(typeof(ICrudController<,>), typeof(CrudControllerBase<,>));
            // services.AddScoped(typeof(ICrudCopyController<,>), typeof(CrudControllerBase<,>));

            // services.AddScoped(typeof(IHistoricalCrudController<,>), typeof(HistoricalCrudControllerBase<,>));
            
            // services.AddScoped(typeof(IHistoricalCrudReadService<,>), typeof(HistoricalCrudControllerBase<,>));

            // services.AddScoped(typeof(IHistoricalCrudService<,>), typeof(HistoricalCrudServiceBase<,>));
            // // services.AddScoped(typeof(ICrudService<Guid, Account>), typeof(CustomInheritedCrudService<Guid, Account>));
            // services.AddScoped(typeof(ICrudService<,>), typeof(CustomImplementedCrudService<,>));
            // services.AddScoped(typeof(CrudServiceBase<,>));
            // //services.AddScoped(typeof(ICrudService<,>), typeof(CrudServiceBase<,>));

            // services.AddScoped(typeof(ICrudRepository), typeof(CrudRepository));

            // services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));


            // to inject a specific layer for all type of object that implemend IEntity<T>
            //services.AddScoped(typeof(ICrudService<,>), typeof(CustomInheritedCrudService<,>));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
