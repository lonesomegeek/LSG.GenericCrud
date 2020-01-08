using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using Sample.Complete.Models.DTOs;
using Sample.Complete.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Complete.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController :
        ControllerBase,
        ICrudController<Guid, AccountDto>,
        IHistoricalCrudController<Guid, AccountDto>
    {
        private readonly IHistoricalCrudController<Guid, AccountDto> _controller;
        private readonly IHistoricalCrudRestoreController<Guid, AccountDto> _restoreController;
        private readonly IHistoricalCrudService<Guid, AccountDto> _service;

        public AccountsController(
            IHistoricalCrudController<Guid, AccountDto> controller,
            IHistoricalCrudRestoreController<Guid, AccountDto> restoreController,
            IHistoricalCrudService<Guid, AccountDto> service)
        {
            _controller = controller;
            _restoreController = restoreController;

            _service = service;
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
        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetHistory(Guid id) => await _controller.GetHistory(id);
        [HttpPost("{id}/restore")]
        public async Task<IActionResult> Restore(Guid id) => await _restoreController.RestoreFromDeletedEntity(id);
    }
}
