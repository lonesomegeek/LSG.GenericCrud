using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using Sample.DataFiller.Models.Entities;

namespace Sample.DataFiller.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : CrudController<Account>
    {
        public AccountsController(ICrudService<Account> service) : base(service) { }
    }
}
