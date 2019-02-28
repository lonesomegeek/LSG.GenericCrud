using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.DataFillers;
using LSG.GenericCrud.Extensions.Controllers;
using LSG.GenericCrud.Extensions.DataFillers;
using LSG.GenericCrud.Extensions.Services;
using LSG.GenericCrud.Helpers;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using WebApplication1.Models;
using WebApplication1.Repositories;
using IUserInfoRepository = LSG.GenericCrud.Services.IUserInfoRepository;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // to activate mvc service
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            // to load an InMemory EntityFramework context
            //services.AddDbContext<TestContext>(opt => opt.UseInMemoryDatabase());
            services.AddDbContext<TestContext>(opt => opt.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MySampleDb;Trusted_Connection=True;"));
            services.AddTransient<IDbContext, TestContext>();
            // inject needed service and repository layers
            services.AddScoped(typeof(ICrudController<>), typeof(CrudController<>));
            services.AddScoped(typeof(IHistoricalCrudController<>), typeof(HistoricalCrudController<>));
            services.AddScoped(typeof(IReadeableCrudController<>), typeof(ReadeableCrudController<>));
            services.AddScoped(typeof(IReadeableCrudService<>), typeof(ReadeableCrudService<>));
            services.AddScoped(typeof(IHistoricalCrudService<>), typeof(HistoricalCrudService<>));

            //services.AddTransient<IEntityDataFiller, ByDataFiller>();
            //services.AddTransient<IEntityDataFiller, DateDataFiller>();
            services.AddTransient<IEntityDataFiller, CreatedFiller>();
            services.AddTransient<IEntityDataFiller, ModifiedFiller>();
            services.AddTransient<IUserInfoRepository, MyUserInfoRepository>();
            //services.AddScoped<IReadeableCrudOptions, ReadeableCrudOptions>();

            services.AddScoped(typeof(ICrudService<,>), typeof(CrudService<,>));
            services.AddScoped(typeof(IHistoricalCrudService<,>), typeof(HistoricalCrudService<,>));
            services.AddScoped(typeof(ICrudController<,>), typeof(CrudController<,>));
            services.AddScoped(typeof(IHistoricalCrudController<,>), typeof(HistoricalCrudController<,>));
            //services.AddScoped(typeof(ICrudService<int, MyIntEntity>), typeof(CrudService<int, MyIntEntity>));

            services.AddCrud();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // activate mvc routing
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
