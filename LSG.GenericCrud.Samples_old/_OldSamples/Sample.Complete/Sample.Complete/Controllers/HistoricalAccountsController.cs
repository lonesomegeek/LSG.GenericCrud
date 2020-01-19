using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using Sample.Complete.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Complete.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricalAccountsController :
        ControllerBase,
        ICrudController<Account>,
        IHistoricalCrudController<Account>
    {
        private readonly IHistoricalCrudController<Account> _controller;
        private readonly IHistoricalCrudRestoreController<Account> _restoreController;

        public HistoricalAccountsController(
            IHistoricalCrudController<Account> controller,
            IHistoricalCrudRestoreController<Account> restoreController)
        {
            _controller = controller;
            _restoreController = restoreController;
        }

        [HttpHead("{id}")]
        public async Task<IActionResult> HeadById(Guid id) => await _controller.HeadById(id);

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
        public async Task<IActionResult> Restore(Guid id) => await _restoreController.RestoreFromDeletedEntity(id);
    }
}
