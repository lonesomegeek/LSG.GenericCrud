using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Dto.Services;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using Sample.Dto.Models.DTOs;
using Sample.Dto.Models.Entities;

namespace Sample.Dto.Controllers
{
    [Route("api/[controller]")]
    public class AccountsEntityController :
        ControllerBase,
        ICrudController<Account>
    {
        private readonly IHistoricalCrudController<Account> _controller;

        public AccountsEntityController(IHistoricalCrudController<Account> controller)
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
}
