# Breaking changes from v4.* to v5.*
- Extension project no longer exists
Not used and not maintained

- You can no longer use ICrudX\<MyEntity\>, you need to use ICrudX\<Type, MyEntity\>. You need to refer to entity type in typings ie.:
    | Where                   | Before                                                                                  | After                                                                                     |
    |-------------------------|-----------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------|
    | In controller class     | ICrudController\<MyEntity\>                                                             | ICrudController\<Guid, MyEntity\>                                                         |
    | In custom service class | ICrudServce\<MyEntity\>                                                                 | ICrudService\<Guid, MyEntity\>                                                            |
    | In Startup.cs           | services.AddScoped(typeof(ICrudService\<\>), typeof(CustomImplementedCrudService\<\>)); | services.AddScoped(typeof(ICrudService\<,\>), typeof(CustomImplementedCrudService\<,\>)); |

- Changing base class definitions
    | Before                   | After                        |
    |--------------------------|------------------------------|
    | CrudController           | CrudControllerBase           |
    | HistoricalCrudController | HistoricalCrudControllerBase |
    | CrudService              | CrudServiceBase              |
    | HistoricalCrudService    | HistoricalCrudServiceBase    |
- The library is now .NET Standard 2.1 compliant. This means that your application using my library must target .NET Core 3.0 and above (https://docs.microsoft.com/en-us/dotnet/standard/net-standard#net-implementation-support). If you need support for .NET Standard 2.0, use the v4.* instead.
