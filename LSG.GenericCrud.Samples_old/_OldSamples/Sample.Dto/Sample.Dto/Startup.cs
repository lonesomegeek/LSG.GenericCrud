﻿using System;
using LSG.GenericCrud.Dto.Helpers;
using LSG.GenericCrud.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sample.Dto.Models;
using Sample.Dto.Models.DTOs;
using Sample.Dto.Models.Entities;
using LSG.GenericCrud.Services;
using LSG.GenericCrud.Dto.Services;
using LSG.GenericCrud.Helpers;
using LSG.GenericCrud.Controllers;
using Sample.Dto.Repositories;

namespace Sample.Dto
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var automapperConfiguration = new AutoMapper.MapperConfiguration(_ =>
            {
                _.CreateMap<AccountDto, Account>()
                    .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FullName.Split(',', StringSplitOptions.None)[0]))
                    .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.FullName.Split(',', StringSplitOptions.None)[1]));
                _.CreateMap<Account, AccountDto>()
                    .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => $"{src.FirstName},{src.LastName}"));
            });
            services.AddSingleton(automapperConfiguration.CreateMapper());


            // to activate mvc service
            services.AddMvc();
            // to load an InMemory EntityFramework context
            services.AddDbContext<SampleContext>(opt => opt.UseInMemoryDatabase());
            services.AddTransient<IDbContext, SampleContext>();

            // My ByDataFiller is using an external repository that will provide information about the actual user
            services.AddTransient<IUserInfoRepository, UserInfoRepository>();

            // to dynamically inject any type of Crud repository of type T in any controllers
            services.AddScoped(typeof(ICrudService<Guid, AccountDto>), typeof(CrudService<Guid, AccountDto, Account>));
            services.AddScoped(typeof(IHistoricalCrudService<Guid, AccountDto>), typeof(HistoricalCrudService<Guid, AccountDto, Account>));
            services.AddScoped(typeof(ICrudService<AccountDto>), typeof(CrudService<Guid, AccountDto, Account>));

            services.AddCrud();
            services.AddCrudDto();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // activate mvc routing
            app.UseMvc();
        }
    }
}
