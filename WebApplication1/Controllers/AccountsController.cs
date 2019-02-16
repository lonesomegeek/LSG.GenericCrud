using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
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

        public Task<IActionResult> HeadById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}