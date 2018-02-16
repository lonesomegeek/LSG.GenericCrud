using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using Sample.Async.Models;

namespace Sample.Async.Controllers
{
    [Route("api/[controller]")]
    public class AccountsAsyncController : CrudController<Account>
    {
        public AccountsAsyncController(ICrudService<Account> service) : base(service) { }
    }
}