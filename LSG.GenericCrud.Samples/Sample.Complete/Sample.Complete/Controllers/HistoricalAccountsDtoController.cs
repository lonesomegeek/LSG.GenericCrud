using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using Sample.Complete.Models.DTOs;
using Sample.Complete.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Complete.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricalAccountsDtoController :
        ControllerBase,
        ICrudController<AccountDto>,
        IHistoricalCrudController<AccountDto>
    {
        private readonly IHistoricalCrudController<AccountDto> _controller;

        public HistoricalAccountsDtoController(IHistoricalCrudController<AccountDto> controller)
        {
            _controller = controller;
        }

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
        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetHistory(Guid id) => await _controller.GetHistory(id);
        [HttpPost("{id}/restore")]
        public async Task<IActionResult> Restore(Guid id) => await _controller.Restore(id);
    }
}
