using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Helpers;
using LSG.GenericCrud.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sample.GenericCrud.Models;

namespace Sample.GenericCrud
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
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
