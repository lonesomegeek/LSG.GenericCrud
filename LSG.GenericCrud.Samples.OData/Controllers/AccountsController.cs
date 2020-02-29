using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Samples.OData.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Samples.OData.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : 
        ControllerBase,
        ICrudController<Guid, Account>
    {
        private readonly ICrudController<Guid, Account> _controller;

        public AccountsController(ICrudController<Guid, Account> controller)
        {
            _controller = controller;
        }
        public Task<ActionResult<Account>> Create([FromBody] Account entity)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Account>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [EnableQuery()]
        public async Task<ActionResult<IEnumerable<Account>>> Get()
        {
            return await _controller.GetAll();
            return new List<Account>
            {
                new Account { Id = Guid.Parse("d74f455c-eb06-4997-8362-01ad89980f01"), Name = "Test1" },
                new Account { Id = Guid.Parse("d74f455c-eb06-4997-8362-01ad89980f02"), Name = "Test2" },
                new Account { Id = Guid.Parse("d74f455c-eb06-4997-8362-01ad89980f03"), Name = "Test3" },
                new Account { Id = Guid.Parse("d74f455c-eb06-4997-8362-01ad89980f04"), Name = "Test4" },
                new Account { Id = Guid.Parse("d74f455c-eb06-4997-8362-01ad89980f05"), Name = "Test5" },
                new Account { Id = Guid.Parse("d74f455c-eb06-4997-8362-01ad89980f06"), Name = "Test6" },
                new Account { Id = Guid.Parse("d74f455c-eb06-4997-8362-01ad89980f07"), Name = "Test7" },
                new Account { Id = Guid.Parse("d74f455c-eb06-4997-8362-01ad89980f08"), Name = "Test8" },
                new Account { Id = Guid.Parse("d74f455c-eb06-4997-8362-01ad89980f09"), Name = "Test9" },
                new Account { Id = Guid.Parse("d74f455c-eb06-4997-8362-01ad89980f10"), Name = "Test10" },
                new Account { Id = Guid.Parse("d74f455c-eb06-4997-8362-01ad89980f11"), Name = "Test11" },
            };
        }

        public Task<ActionResult<IEnumerable<Account>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Account>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> HeadById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Update(Guid id, [FromBody] Account entity)
        {
            throw new NotImplementedException();
        }
    }
}
