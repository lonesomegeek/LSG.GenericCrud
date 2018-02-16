using LSG.GenericCrud.Helpers;
using LSG.GenericCrud.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sample.Models;

namespace generic_crud
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<SampleContext>(opt => opt.UseInMemoryDatabase());
            services.AddTransient<IDbContext, SampleContext>();

            services.AddCrud();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
