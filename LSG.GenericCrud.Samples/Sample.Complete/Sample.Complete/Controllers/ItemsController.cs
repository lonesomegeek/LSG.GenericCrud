using LSG.GenericCrud.Controllers;
using Microsoft.AspNetCore.Mvc;
using Sample.Complete.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Complete.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController :
        ControllerBase,
        IHistoricalCrudController<Guid, Item>
    {
        private readonly IHistoricalCrudController<Guid, Item> _controller;

        public ItemsController(IHistoricalCrudController<Guid, Item> controller)
        {
            _controller = controller;
        }

        [HttpHead("{id}")]
        public async Task<IActionResult> HeadById(Guid id) => await _controller.HeadById(id);

        [HttpPost]
        public async Task<ActionResult<Item>> Create([FromBody] Item entity) => await _controller.Create(entity);
        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> Delete(Guid id) => await _controller.Delete(id);
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetAll() => await _controller.GetAll();
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<Item>> GetById(Guid id) => await _controller.GetById(id);
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Item entity) => await _controller.Update(id, entity);
        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetHistory(Guid id) => await _controller.GetHistory(id);
        [HttpPost("{id}/restore")]
        public async Task<IActionResult> Restore(Guid id) => throw new NotImplementedException();
    }
}
