- Extension project no longer exists
Not used and not maintained

- You can no longer use ICrudX\<MyEntity\>, you need to use ICrudX\<Type, MyEntity\>. You need to refer to entity type in typings ie.:
    | Where | Before | After |
    | ----- | ------ | ------- |
    | In controller class | ICrudController\<MyEntity\> | ICrudController\<Guid, MyEntity\> |
    | In custom service class | ICrudServce\<MyEntity\> | ICrudService\<Guid, MyEntity\> |
    | In Startup.cs | services.AddScoped(typeof(ICrudService\<\>), typeof(CustomImplementedCrudService\<\>)); | services.AddScoped(typeof(ICrudService\<,\>), typeof(CustomImplementedCrudService\<,\>)); |

- CrudController now CrudControllerBase, HistoricalCrudController now, ... CrudService, HistoricalCrudService