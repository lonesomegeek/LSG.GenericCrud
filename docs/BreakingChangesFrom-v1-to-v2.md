# Breaking changes from v1.* to v2.*
- CrudControllers using standard CRUD now needs a service layer instead of a repository in the class constructor
   - Before: ```public AccountsAsyncController(Crud<Account> dal) : base(dal) { }```
   - After: ```public AccountsAsyncController(ICrudService<Account> service) : base(service) { }```
- CrudControllers using historical CRUD now needs to use a service layer instead of a repository in the class constructor
   - Before: ```public AccountsController(HistoricalCrud<Account> dal) : base(dal) { }```
   - After: ```public AccountsController(IHistoricalCrudService<Account> service) : base(service) { }```
- CrudControllers using standard CRUD with DTO Feature are now simplier
   - Before:
   ```csharp
    [Route("api/[controller]")]
    public class AccountsDtoController : CrudController<AccountDto, Account>
    {
        public AccountsDtoController(Crud<Account> dal, IMapper mapper) : base(dal, mapper){}
    }
    ```

   - After:
   ```csharp
   [Route("api/[controller]")]
    public class AccountsDtoController : CrudController<AccountDto>
    {
        public AccountsDtoController(CrudService<AccountDto, Account> service) : base(service) { }
    }    
    ```
- CrudController using historical CRUD with DTO Feature are now simplier
    ```csharp
    [Route("api/[controller]")]
    public class AccountsController : HistoricalCrudController<AccountDto>
    {
        public AccountsController(HistoricalCrudService<AccountDto, Account> service) : base(service) { }
    }
    ```
- Custom service layer is supported in a different way (ICrud is not available anymore), see complete sample for more information.
- Custom repository layer is not supported anymore, create your own service layer to customize repository layer.

- Startup configuration now requires to be explicit on service and repository layers injection
   - Before: 
   ```csharp
   services.AddScoped(typeof(Crud<>));
   ```

   - After:
   ```csharp
    services.AddScoped(typeof(ICrudService<>), typeof(CrudService<>));
    services.AddScoped(typeof(ICrudRepository<>), typeof(CrudRepository<>));
    ```

- Startup configuration now requires to be explicit on service layer with dto feature
   - Before: Nothing
   - After: 
   ```csharp
   services.AddScoped(typeof(CrudService<,>));
   ```
   
   and/or
   
   ```csharp
   services.AddScoped(typeof(HistoricalCrudService<,>));
   ```
