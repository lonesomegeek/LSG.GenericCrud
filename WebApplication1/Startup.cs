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
using WebApplication1.Models;
using WebApplication1.Repositories;

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

            services.AddTransient<IEntityDataFiller, ByDataFiller>();
            services.AddTransient<IEntityDataFiller, DateDataFiller>();
            services.AddTransient<LSG.GenericCrud.Extensions.DataFillers.IUserInfoRepository, MyUserInfoRepository>();
            services.AddScoped<IReadeableCrudOptions, ReadeableCrudOptions>();

            services.AddCrud();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // activate mvc routing
            app.UseMvc();
        }
    }
}
