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
    public class AccountsController :
        ControllerBase,
        ICrudController<AccountDto>
    {
        private readonly IHistoricalCrudController<AccountDto> _controller;

        public AccountsController(IHistoricalCrudController<AccountDto> controller)
        {
            _controller = controller;
        }

        [HttpHead("{id}")]
        public async Task<IActionResult> HeadById(Guid id) => await _controller.HeadById(id);

        [HttpPost]
        public async Task<ActionResult<AccountDto>> Create([FromBody] AccountDto dto) => await _controller.Create(dto);
        [HttpDelete("{id}")]
        public async Task<ActionResult<AccountDto>> Delete(Guid id) => await _controller.Delete(id);
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAll() => await _controller.GetAll();
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<AccountDto>> GetById(Guid id) => await _controller.GetById(id);
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AccountDto dto) => await _controller.Update(id, dto);
    }
}
