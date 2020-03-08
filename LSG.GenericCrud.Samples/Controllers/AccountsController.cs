using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Samples.Controllers
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

        [HttpPost]
        public Task<ActionResult<Account>> Create([FromBody] Account entity) => _controller.Create(entity);

        [HttpDelete("{id}")]
        public Task<ActionResult<Account>> Delete(Guid id) => _controller.Delete(id);

        [HttpGet()]
        public Task<ActionResult<IEnumerable<Account>>> GetAll() => _controller.GetAll();

        [HttpGet("{id}")]
        public Task<ActionResult<Account>> GetById(Guid id) => _controller.GetById(id);

        [HttpHead("{id}")]
        public Task<IActionResult> HeadById(Guid id) => _controller.HeadById(id);

        [HttpPut("{id}")]
        public Task<IActionResult> Update(Guid id, [FromBody] Account entity) => _controller.Update(id, entity);
    }

    public class Account : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
