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
using LSG.GenericCrud.TestApi.Controllers;
using LSG.GenericCrud.TestApi.Models.DTOs;
using LSG.GenericCrud.TestApi.Models.Entities;
using LSG.GenericCrud.TestApi.Repositories;

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
            //http://dotnetthoughts.net/using-automapper-in-aspnet-core-project/
            var automapperConfiguration = new AutoMapper.MapperConfiguration(_ =>
            {
                _.CreateMap<CarrotDto, Carrot>().ForMember(
                    dest => dest.Color,
                    opts => opts.MapFrom(src => src.Colorification));
                _.CreateMap<Carrot, CarrotDto>().ForMember(
                    dest => dest.Colorification,
                    opts => opts.MapFrom(src => src.Color));
            });
            var mapper = automapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
            
            // Add framework services.
            services.AddMvc();

            services.AddDbContext<MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IDbContext, MyContext>();

            services.AddScoped<CustomDal>();

            services.AddTransient<IEntityDataFiller<BaseEntity>, DateDataFiller>();
            services.AddTransient<IEntityDataFiller<BaseEntity>, ByDataFiller>();
            services.AddTransient<IUserInfoRepository, UserInfoRepository>();

            services.AddScoped(typeof(Crud<>));
            services.AddScoped(typeof(HistoricalCrud<>));

            services.AddTransient<ICrud<CustomCarrot>, CustomCarrotDal>();

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
