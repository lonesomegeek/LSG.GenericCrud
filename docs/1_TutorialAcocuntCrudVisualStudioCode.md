LSG.GenericCrud - Visual Studio Code Simple Scenario
=

At this end of this tutorial, your Visual Studio Code workspace should look like this:

![](img/2017-09-04-14-30-29.png)

and you will have these routes available for the account entity

| VERB   | URL               | Description           |
|--------|-------------------|-----------------------|
| GET    | /api/accounts     | Retreive all accounts |
| GET    | /api/accounts/:id | Retreive one account  |
| POST   | /api/accounts     | Create one account    |
| PUT    | /api/accounts/:id | Update one account    |
| DELETE | /api/accounts/:id | Delete one account    |

## Getting started
Using the command line, execute the following commands:
- Create a new **dotnet core web** app

    ```bash
    dotnet new webapi -o Sample.GenericCrud
    ```

- Enter newly created folder

    ```bash
    cd Sample.GenericCrud
    ```

- Add requried LSG.GenericCrud library

    ```bash
    dotnet add package LSG.GenericCrud
    dotnet add package Microsoft.EntityFrameworkCore.InMemory
    ```

- Open visual studio code to continue the next steps

    ```bash
    code .
    ```

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

- Create a new folder named: **Controllers**

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

- In a terminal, execute the project

```bash
dotnet run
```

- Go to **http://localhost:5000/api/accounts**, and you are done!

## Testing!
Here is a [Postman](https://www.getpostman.com/) collection to test your new RESTful CRUD api

[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/090af27316cd23c61951)

Note: If you want to use this postman collection, you will have to change the Web Service Settings (App URL) to use **port 5000**.