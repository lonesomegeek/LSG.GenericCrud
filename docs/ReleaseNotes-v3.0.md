# Release Notes - v3.0

- LSG.GenericCrud
    - Switch from an inheritance model (controllers, services) to a composition model
    - *Async* classes have been removed and all base code is now async by default (controllers, services, repositories)
- LSG.GenericCrud.Extensions
    - Adding IReadeableCrud*, to let have user info about new stuff in entities dynamically *more documentation will come soon*
    - Implement data filler async functions
- Samples
    - Remove Async Sample (because base classes are async now)
    - Remove Async versions in all samples (because base classes are async now)
    - Adding new applicatino sample *in development, not ready yet*
    - Update all samples to manage new architecture
- Tests
    - Update all tests to manage new architecture

## v3.0 new architecture
The simpliest way to explain the changes of the new version is to compare them side by side. If you need more complete samples, take a look [at the samples](../LSG.GenericCrud.Samples/README.md).

You may say that the new version needs a little more code, but is way more flexible. And... let's say that a little *copy/paste* can't hurt that much! :)

### Idem (inheritance) from v2.0 CrudController\<T> and v3.0

To make things simple, I've supported the inheritance for the simpliest CRUD scenario through CrudController\<T> in the v3.0.

Before (v2.*)
```csharp
[Route("api/[controller]")]
public class AccountsController : CrudController<Account>
{
    public AccountsController(ICrudService<Account> service) : base(service)
    {
    }
}
```

After (v3.*)
```csharp
[Route("api/[controller]")]
public class AccountsController : CrudController<Account>
{
    public AccountsController(ICrudService<Account> service) : base(service)
    {
    }
}
```

### Composition with CrudController\<T>

CrudController\<T> with composition.

Before (v2.*)
```csharp
[Route("api/[controller]")]
public class AccountsController : CrudController<Account>
{
    public AccountsController(ICrudService<Account> service) : base(service)
    {
    }
}
```

After (v3.*)
```csharp
[Route("api/[controller]")]
[ApiController]
public class AccountsController :
    ControllerBase,
    ICrudController<Account>
{
    private readonly ICrudController<Account> _controller;

    public AccountsController(ICrudController<Account> controller)
    {
        _controller = controller;
    }

    [HttpPost]
    public async Task<ActionResult<Account>> Create([FromBody] Account entity) => await _controller.Create(entity);
    [HttpDelete("{id}")]
    public async Task<ActionResult<Account>> Delete(Guid id) => await _controller.Delete(id);
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Account>>> GetAll() => await _controller.GetAll();
    [Route("{id}")]
    [HttpGet]
    public async Task<ActionResult<Account>> GetById(Guid id) => await _controller.GetById(id);
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Account entity) => await _controller.Update(id, entity);
}
```

### Composition with HistoricalCrudController\<T>

HistoricalCrudController\<T> with composition.

Before (v2.*)
```csharp
[Route("api/[controller]")]
public class AccountsController : HistoricalCrudController<Account>
{
    public AccountsController(IHistoricalCrudService<Account> service) : base(service)
    {
    }
}
```

After (v3.*)
```csharp
[Route("api/[controller]")]
[ApiController]
public class AccountsController :
    ControllerBase,
    ICrudController<Account>,
    IHistoricalCrudController<Account>
{
    private readonly IHistoricalCrudController<Account> _controller;

    public AccountsController(IHistoricalCrudController<Account> controller)
    {
        _controller = controller;
    }

    [HttpPost]
    public async Task<ActionResult<Account>> Create([FromBody] Account entity) => await _controller.Create(entity);
    [HttpDelete("{id}")]
    public async Task<ActionResult<Account>> Delete(Guid id) => await _controller.Delete(id);
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Account>>> GetAll() => await _controller.GetAll();
    [Route("{id}")]
    [HttpGet]
    public async Task<ActionResult<Account>> GetById(Guid id) => await _controller.GetById(id);
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Account entity) => await _controller.Update(id, entity);
    [HttpGet("{id}/history")]
    public async Task<IActionResult> GetHistory(Guid id) => await _controller.GetHistory(id);
    [HttpPost("{id}/restore")]
    public async Task<IActionResult> Restore(Guid id) => await _controller.Restore(id);
}
```