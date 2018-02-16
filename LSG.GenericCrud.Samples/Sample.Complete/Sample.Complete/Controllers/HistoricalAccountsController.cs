using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using Sample.Complete.Models.Entities;

namespace Sample.Complete.Controllers
{
    [Route("api/[controller]")]
    public class HistoricalAccountsController : HistoricalCrudController<Account>
    {
        public HistoricalAccountsController(IHistoricalCrudService<Account> service) : base(service) { }
    }
}
