using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Helpers;
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
using LSG.GenericCrud.Controllers;
using Sample.HistoricalCrud.Repositories;

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
            
            // My ByDataFiller is using an external repository that will provide information about the actual user
            services.AddTransient<IUserInfoRepository, UserInfoRepository>();

            // LSG.GenericCrud generics injection
            services.AddCrud();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // activate mvc routing
            app.UseMvc();
        }
    }
}
