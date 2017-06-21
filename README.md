# generic-crud

# Prerequisites
You need at least:
- a running instance of SQL server on your localhost
- Visual Studio 2017

# Introduction
This library is used to provide CRUD operations with a Generic Crud Controller with a Generic Repository. This library allies injection (IoC), minimum codebase for maximum efficiency.

# What is CRUD?

Sample URLs:
- GET api/products (should return all created product entities)
- POST api/products (create a product entity)
- GET api/products/{id} (should return a single created product entity)
- PUT api/products/{id} (updates a created product entity)
- DELETE api/products/{id} (deletes a product entity)
- GET api/customers (should return all created customers)
- GET api/customers/{id} (should return a single created customer)

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