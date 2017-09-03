# generic-crud

# Prerequisites
You need at least:
- Visual Studio 2017
- Visual Studio Code

# Introduction
This library is used to provide CRUD operations with a Generic Crud Controller with a Generic Repository. This library allies injection (IoC), minimum codebase for maximum efficiency.

# What is CRUD?

Sample URLs:
- GET api/products (return all products)
- POST api/products (create a product)
- GET api/products/{id} (return a single product)
- PUT api/products/{id} (updates a product)
- DELETE api/products/{id} (deletes a product)
- GET api/customers (return all customers)
- GET api/customers/{id} (return a single customer)

# Getting started (simple scenario)

This is the most simple scenario. To have more integration scenario, see the docs.

1. Create a new ASP.NET Core Web Application (be sure to target ASP.NET Core 1.1+)
2. In the template selection, select Empty, click OK
3. When the solution is ready, go to Package Manager Console and execute the following command: `Install-Package LSG.GenericCrud Microsoft.EntityFrameworkCore.InMemory`
4. In Startup.cs
   1. In ConfigureServices method, add the following lines:
      
      ```
      services.AddMvc();
      services.AddScoped(typeof(Crud<>));
      ```
5. Create new Controller, SampleController and:
   1. Replace `: Controller` by `: CrudController<Sample>`
   2. Implement an constructor override that should look like this: TODO
   3. Your controller should look like this: TODO

6. Create a new class `Sample` to represent your entity
   1. Create class Sample.cs
   2. Paste this to replace your actual empty class definition: TODO
   3. We are adding the bare minimum to this object, an Id of type Guid and a Value of type string.

7. Create a fake SampleRepository. Its role will be to return fake Sample objects (that are retreived with a database in a real scenario)
   1. Create class SampleRepository.cs
   2. Paste this to replace your actual empty class definition: TODO
   3. We are adding the bare minimum to provide access to the in-memory dataset and to be able to retreive an asset by its id.

More documentation is available ....

# Sample: Create a CRUD ressource
1. Create new entity to map in data model
    1. Open Model1.cs
    2. Add new entity, it must implement interface `IEntity`
    ```cs
    public class Test : IEntity
    {
        public Test()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Value { get; set; }
    }
    ```
	3. Add DbSet in data model (in code first, new doc will come for database first strategy)
	```cs
	public virtual DbSet<Test> Tests { get; set; }
	```
	4. For sample purposes, delete the targetting database before launching project.
2. Create a new controller that can map this new ressource
    1. Create new controller TestController, the controller must:
        - inherit from `CrudController<Test>`
        - inject a `Crud<Test>` data access layer in constructor
        ```cs    
        [Route("api/[controller]")]
        public class TestController : CrudController<Test>
        {
            public TestController(Crud<Test> dal) : base(dal) { }
        }
        ```
    2. Add missing usings in file
3. Enjoy your new REST Ressource!
    1. Press F5
    2. You can use Postman or other REST tools to create a new Test resource 

        | VERB | URL            | BODY                 | Description                 |
        |------|----------------|----------------------|-----------------------------|
        | GET  | /api/test      |                      | Get all test ressources     |
        | GET  | /api/test/{id} |                      | Get specifid test ressource |
        | POST | /api/test      | { value: "test123" } | Create a new test ressource |

And that's it!
Enjoy!

Exemples:
https://stormpath.com/blog/tutorial-entity-framework-core-in-memory-database-asp-net-core

# Release Notes

- v1.1.0: .NET Core 2.0 compatibility
- v1.0.1: Adding support for interfacable repositories
- v1.0.0: Initial version