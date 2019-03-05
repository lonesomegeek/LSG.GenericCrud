using System;
using AutoMapper;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.DataFillers;
using LSG.GenericCrud.Dto.Services;
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

            services.AddTransient<IEntityDataFiller, CreatedFiller>();
            services.AddTransient<IEntityDataFiller, ModifiedFiller>();
            services.AddTransient<IUserInfoRepository, MyUserInfoRepository>();
            
            services.AddScoped(typeof(ICrudService<,>), typeof(CrudService<,>));
            services.AddScoped(typeof(IHistoricalCrudService<,>), typeof(HistoricalCrudService<,>));
            services.AddScoped(typeof(ICrudController<,>), typeof(CrudController<,>));
            services.AddScoped(typeof(IHistoricalCrudController<,>), typeof(HistoricalCrudController<,>));
            services.AddScoped(typeof(IHistoricalCrudReadService<,>), typeof(HistoricalCrudReadService<,>));
            services.AddScoped<IHistoricalCrudService<Guid, AccountDto>, HistoricalCrudService<Guid, AccountDto, Account>>();
            services.AddScoped<ICrudService<Guid, AccountDto>, CrudService<Guid, AccountDto, Account>>();
            services.AddScoped(typeof(ICrudService<Guid, AccountDto>), typeof(CrudService<Guid, AccountDto, Account>));

            services.AddSingleton(CreateAutoMapperConfiguration());
            services.AddCrud();
        }
        /// <summary>
        /// This method is creating the automapper DTO to entity mapping (and vice versa)
        /// </summary>
        /// <returns></returns>
        private IMapper CreateAutoMapperConfiguration()
        {
            var automapperConfiguration = new MapperConfiguration(_ =>
            {
                _.CreateMap<Account, AccountDto>()
                    .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Pouette, opts => opts.MapFrom(src => src.Description));
                _.CreateMap<AccountDto, Account>()
                    .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Pouette));
            });
            return automapperConfiguration.CreateMapper();
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
