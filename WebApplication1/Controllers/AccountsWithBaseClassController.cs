using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsWithBaseClassController : 
        CrudController<Account>,
        IHistoricalCrudController<Account>
    {
        private readonly IHistoricalCrudController<Account> _historicalCrudCrudController;

        public AccountsWithBaseClassController(
            ICrudService<Account> service,
            IHistoricalCrudController<Account> historicalCrudCrudController) : base(service)
        {
            _historicalCrudCrudController = historicalCrudCrudController;
        }

        [HttpGet("{id}/history")]
        public Task<IActionResult> GetHistory(Guid id) => _historicalCrudCrudController.GetHistory(id);
        [HttpPost("{id}/restore")]
        public Task<IActionResult> Restore(Guid id) => _historicalCrudCrudController.Restore(id);
    }
    
}
