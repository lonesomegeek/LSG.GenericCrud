using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Middlewares;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.TestApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Repositories.DataFillers;

namespace LSG.GenericCrud.TestApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddDbContext<MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IDbContext, MyContext>();

            services.AddTransient<IEntityDataFiller<BaseEntity>, DateDataFiller>();
            services.AddTransient<IEntityDataFiller<BaseEntity>, ByDataFiller>();

            services.AddScoped<Crud<Item>>();
            services.AddScoped<Crud<Carrot>>();
            services.AddScoped<Crud<HistoricalEvent>>();
            services.AddScoped<HistoricalCrud<Carrot>>();
            services.AddScoped<HistoricalCrud<Item>>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var options = new JwtBearerOptions
            {
                Audience = "https://LSG.GenericCrud.TestApi",
                Authority = "https://premiertechieg-dv.auth0.com/",

            };
            app.UseJwtBearerAuthentication(options);

            //app.UseMiddleware<AuthorizationMiddleware>();

            app.UseMvc();

        }
    }
}
