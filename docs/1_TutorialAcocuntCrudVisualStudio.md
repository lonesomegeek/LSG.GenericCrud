LSG.GenericCrud - Visual Studio Simple Scenario
=

At this end of this tutorial, your Visual Studio workspace should look like this:

![](img/2017-09-05-12-22-03.png)

and you will have these routes available for the account entity

| VERB   | URL               | Description           |
|--------|-------------------|-----------------------|
| GET    | /api/accounts     | Retreive all accounts |
| GET    | /api/accounts/:id | Retreive one account  |
| POST   | /api/accounts     | Create one account    |
| PUT    | /api/accounts/:id | Update one account    |
| DELETE | /api/accounts/:id | Delete one account    |

## Prerequisites
- Visual Studio 2019 update 9+ (v16.4+)
- .NET core 3.1 SDK

## Getting started
In Visual Studio, execute the following actions:
- Create a new project:
    - Project Type: **ASP.NET Core Web Application (C#)**
    - Click Next
    - Project name: **Sample.GenericCrud**
    - Click Create

- Select in the next windows 
    - Framework: .NET Core
    - Version: ASP.NET Core 3.1
    - Model: **API**  
    - Click create  
    
- When the project is created, make sure you add a reference to these Nuget packages:
    - LSG.GenericCrud
    - Microsoft.EntityFrameworkCore.InMemory

## Create needed assets
These steps will create needed assets required to make work a simple controller connected to an InMemory EntityFrameworkCore entity CRUD.

Note: For sake of clarity, the documentation does not include references (using). 

### Create data models and database context

- Create a new folder named: **Models**

- Add a new class, in **Models** folder, named: **Account.cs**
    ```csharp
    public class Account : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    ```

- Add a new class, in **Models** folder, named: **SampleContext.cs**
    ```csharp
    public class SampleContext : BaseDbContext, IDbContext
    {
        public SampleContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider) {}

        public DbSet<Account> Accounts { get; set; }
    }
    ```

### Create API controller

- Add a new class, in **Controllers** folder, named: **AccountsController.cs**
    ```csharp
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : CrudControllerBase<Guid, Account>
    {
        public AccountsController(ICrudService<Guid, Account> service) : base(service)
        {
        }
    }
    ```

### Final adjustments
Adjust **Startup.cs** class to enable injection and GenericCrud modules in **ConfigureServices** method. The class should look like this.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // to activate mvc service
    services.AddControllers();
    // to load an InMemory EntityFramework context
    services.AddDbContext<SampleContext>(opt => opt.UseInMemoryDatabase("Sample.GenericCrud"));
    services.AddTransient<IDbContext, SampleContext>();
    // inject needed service and repository layers
    services.AddCrud();
}
```

## Start application

Press F5 (to compile and run the web app). After a while, a web page will open.

- Go to **http://localhost:\<PORT\>/api/accounts**, and you are done!

## Testing!
Here is a [Postman](https://www.getpostman.com/) collection to test your new RESTful CRUD api

[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/090af27316cd23c61951)

Note: If you want to use this postman collection, you will have to change the Web Service Settings (App URL) to use **port 5000**. (right-click project, properties, debug)