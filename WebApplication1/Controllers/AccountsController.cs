using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using DeltaRequest = LSG.GenericCrud.Models.DeltaRequest;

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

        [HttpPost("{id}/copy")]
        public async Task<ActionResult<Account>> Copy(Guid id) => await _controller.Copy(id);

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
        public async Task<IActionResult> RestoreFromDeletedEntity(Guid id) => await _controller.RestoreFromDeletedEntity(id);
        [HttpPost("{entityId}/restore/{changesetId}")]
        public async Task<ActionResult<Account>> RestoreFromChangeset(Guid entityId, Guid changesetId) => await _controller.RestoreFromChangeset(entityId, changesetId);
        [HttpPost("{entityId}/copy/{changesetId}")]
        public async Task<ActionResult<Account>> CopyFromChangeset(Guid entityId, Guid changesetId) => await _controller.CopyFromChangeset(entityId, changesetId);
        [HttpPost("read")]
        public async Task<IActionResult> MarkAllAsRead() => await _controller.MarkAllAsRead();
        [HttpPost("unread")]
        public async Task<IActionResult> MarkAllAsUnread() => await _controller.MarkAllAsUnread();
        [HttpPost("{id}/read")]
        public async Task<IActionResult> MarkOneAsRead(Guid id) => await _controller.MarkOneAsRead(id);
        [HttpPost("{id}/unread")]
        public async Task<IActionResult> MarkOneAsUnread(Guid id) => await _controller.MarkOneAsUnread(id);
        [HttpGet("read-status")]
        public async Task<ActionResult<IEnumerable<ReadeableStatus<Account>>>> GetReadStatus() => await _controller.GetReadStatus();
        [HttpGet("{id}/read-status")]
        public async Task<ActionResult<ReadeableStatus<Account>>> GetReadStatusById(Guid id) => await _controller.GetReadStatusById(id);
        [HttpPost("{id}/delta")]
        public async Task<IActionResult> Delta(Guid id, DeltaRequest request) => await _controller.Delta(id, request);
        [HttpHead("{id}")]
        public async Task<IActionResult> HeadById(Guid id) => await _controller.HeadById(id);
    }
}