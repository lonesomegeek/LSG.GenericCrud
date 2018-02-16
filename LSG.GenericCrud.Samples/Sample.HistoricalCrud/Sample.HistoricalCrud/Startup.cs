using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.HistoricalCrud.Models;
using LSG.GenericCrud.Services;

namespace Sample.HistoricalCrud
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
            // LSG.GenericCrud generics injection
            services.AddScoped(typeof(ICrudService<>), typeof(CrudService<>));
            services.AddScoped(typeof(ICrudRepository), typeof(CrudRepository));
            services.AddScoped(typeof(IHistoricalCrudService<>), typeof(HistoricalCrudService<>));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // activate mvc routing
            app.UseMvc();
        }
    }
}
