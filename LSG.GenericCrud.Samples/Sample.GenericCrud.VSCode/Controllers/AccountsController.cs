using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using Sample.Models;

namespace Sample.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : CrudController<Account>
    {
        public AccountsController(ICrudService<Account> service) : base(service) { }
    }
}