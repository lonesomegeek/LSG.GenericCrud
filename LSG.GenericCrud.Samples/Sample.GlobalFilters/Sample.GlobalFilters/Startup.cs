using System;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.DataFillers;
using LSG.GenericCrud.Helpers;
using LSG.GenericCrud.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.GlobalFilters.DataFillers;
using Sample.GlobalFilters.Models;
using Sample.GlobalFilters.Repositories;

namespace Sample.GlobalFilters
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
            services.AddMvc();
            
            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(60);
                options.Cookie.HttpOnly = true;
            });

            // adding database context stuff
            services.AddDbContext<SampleContext>(opt => opt.UseInMemoryDatabase());
            services.AddTransient<IDbContext, SampleContext>();

            // adding user info repository
            services.AddTransient<IUserInfoRepository, UserInfoRepository>();

            // adding data fillers
            services.AddSingleton<IEntityDataFiller, SoftwareDeleteDataFiller>();
            services.AddSingleton<IEntityDataFiller, SingleTenantDataFiller>();

            // adding lsg.generic crud stack
            services.AddCrud();
            services.AddScoped(typeof(ICrudController<>), typeof(CrudController<>)); // TODO INCLUDE IN ALPHA2
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSession();
            app.UseMvc();
        }
    }
}
