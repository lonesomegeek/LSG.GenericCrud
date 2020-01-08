using AutoMapper;
using LSG.GenericCrud.DataFillers;
using LSG.GenericCrud.Dto.Helpers;
using LSG.GenericCrud.Extensions.DataFillers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using LSG.GenericCrud.Repositories;
using Sample.Complete.Models;
using Sample.Complete.Models.DTOs;
using Sample.Complete.Models.Entities;
using Sample.Complete.Repositories;
using LSG.GenericCrud.Services;
using LSG.GenericCrud.Helpers;
using LSG.GenericCrud.Controllers;
using System;
using LSG.GenericCrud.Dto.Services;
using Sample.Complete.Services;
using Sample.Complete.Models.DataFillers;

namespace Sample.Complete
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
            services.AddSingleton(CreateAutoMapperConfiguration());
            
            // Add framework services.
            services.AddMvc();

            // Specifies to use this context with an InMemory Database connection
            //services.AddDbContext<MyContext>(opt => opt.UseInMemoryDatabase());
            services.AddDbContext<MyContext>(opt => opt.UseSqlServer("server=localhost;Initial Catalog=LSG.GenericCrud.Sample.Complete;Integrated Security=True;"));

            // Map our dynamic repository to our custom context
            services.AddTransient<IDbContext, MyContext>();

            // Add data fillers
            services.AddTransient<IEntityDataFiller, CreatedFiller>();
            services.AddTransient<IEntityDataFiller, SoftwareDeleteByStatusDataFiller>();

            // My ByDataFiller is using an external repository that will provide information about the actual user
            services.AddTransient<IUserInfoRepository, UserInfoRepository>();

            // LSG.GenericCrud generics injection
            services.AddScoped(typeof(ICrudService<Guid, AccountDto>), typeof(CrudService<Guid, AccountDto, Account>));
            services.AddScoped(typeof(IHistoricalCrudService<Guid, AccountDto>), typeof(AccountService));

            services.AddScoped<IHistoricalCrudController<Guid, AccountDto>, HistoricalCrudController<Guid, AccountDto>>();
            services.AddScoped<IHistoricalCrudRestoreController<Guid, AccountDto>, HistoricalCrudController<Guid, AccountDto>>();
            
            services.AddCrud();
            services.AddCrudDto();
        }

        /// <summary>
        /// This method is creating the automapper DTO to entity mapping (and vice versa)
        /// </summary>
        /// <returns></returns>
        private IMapper CreateAutoMapperConfiguration()
        {
            var automapperConfiguration = new MapperConfiguration(_ =>
            {
                _.CreateMap<AccountDto, Account>().ForMember(
                    dest => dest.Name,
                    opts => opts.MapFrom(src => src.FullName));
                _.CreateMap<Account, AccountDto>().ForMember(
                    dest => dest.FullName,
                    opts => opts.MapFrom(src => src.Name));
            });
            return automapperConfiguration.CreateMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            // database initialization
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<MyContext>();
                context.Database.EnsureCreated();
            }
        }
    }
}
