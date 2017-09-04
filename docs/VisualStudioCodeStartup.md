LSG.GenericCrud - Visual Studio Code Sample #1
=
This is sample show all the steps required to use this library using Visual Studio Code.

## Getting started
Using the command line, execute the following commands:
- Create a new **dotnet core web** app

    ```bash
    dotnet new web -o Sample.GenericCrud
    ```

- Enter newly created folder

    ```bash
    cd Sample.GenericCrud
    ```

- Add requried LSG.GenericCrud library

    ```bash
    dotnet add package LSG.GenericCrud --version 1.1.0
    ```

- Open visual studio code to continue the next steps

    ```bash
    code .
    ```

## Create needed assets
These steps will create needed assets required to make work a simple controller connected to an InMemory EntityFrameworkCore entity CRUD.

add Models folder

add new entity class Account.cs

    ```
    using System;
    using LSG.GenericCrud.Models;

    namespace Sample.Models
    {
        public class Account : IEntity
        {
            public Account()
            {
                Id = Guid.NewGuid();
            }
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
    }
    ```
add new  new db context (that will be loaded InMemory for the sample)

- create in Models folder the class SampleContext.cs

    ```
    using System;
    using LSG.GenericCrud.Repositories;
    using Microsoft.EntityFrameworkCore;

    namespace Sample.Models
    {
        public class SampleContext : BaseDbContext, IDbContext
        {
            public SampleContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider) {}

            public DbSet<Account> Accounts { get; set; }
        }
    }
    ```

add controllers folder

add new controller AccountsController.cs

    ```
    using LSG.GenericCrud.Controllers;
    using LSG.GenericCrud.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Sample.Models;

    namespace Sample.Controllers
    {
        [Route("api/[controller]")]
        public class AccountsController : CrudController<Account>
        {
            public AccountsController(Crud<Account> dal) : base(dal) { }
        }
    }
    ```

Adjust Startup.cs to enable injection and GenericCrud modules

    ```
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<SampleContext>(opt => opt.UseInMemoryDatabase());
            services.AddTransient<IDbContext, SampleContext>();

            services.AddScoped(typeof(Crud<>));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
    ```

app.usemvc instead of app.Run...?!?!

dotnet restore

dotnet build

dotnet run