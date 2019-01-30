using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.DataFillers;
using LSG.GenericCrud.Helpers;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sample.DataFiller.Models;
using Sample.DataFiller.Models.DataFillers;
namespace Sample.DataFiller
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEntityDataFiller, Models.DataFillers.DateDataFiller>();
            services.AddTransient<IEntityDataFiller, Models.DataFillers.ByDataFiller>();

            // to activate mvc service
            services.AddMvc();
            // to load an InMemory EntityFramework context
            services.AddDbContext<SampleContext>(opt => opt.UseInMemoryDatabase());
            services.AddTransient<IDbContext, SampleContext>();
            // to dynamically inject any type of Crud repository of type T in any controllers
            services.AddCrud();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // activate mvc routing
            app.UseMvc();
        }
    }
}
