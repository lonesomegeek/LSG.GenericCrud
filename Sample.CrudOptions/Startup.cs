using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Dto.Helpers;
using LSG.GenericCrud.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Sample.CrudOptions
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
            // with this, you have the certitude that any controllers will be protected with the authorization policy
            // https://stackoverflow.com/questions/36413476/mvc-core-how-to-force-set-global-authorization-for-all-actions
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddRequirements(new CrudRequirement()) // instead of having to declare [Authorize(Policy = "CrudDefaultHandler")], this is doing it for ALL controllers
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddCrud();
            services.AddCrudDto();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // enable MVC 
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
