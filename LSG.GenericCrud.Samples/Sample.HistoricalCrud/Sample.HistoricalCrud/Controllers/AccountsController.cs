using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using Sample.HistoricalCrud.Models;

namespace Sample.HistoricalCrud.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : HistoricalCrudController<Account>
    {
        public AccountsController(IHistoricalCrudService<Account> service) : base(service) { }
    }
}
