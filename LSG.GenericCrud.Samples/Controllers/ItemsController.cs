using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Samples.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController :
            ControllerBase,
            ICrudController<Guid, ItemDto> {
        private readonly ICrudController<Guid, ItemDto> _controller;

        public ItemsController(ICrudController<Guid, ItemDto> controller)
        {
            _controller = controller;
        }

        [HttpPost]
        public Task<ActionResult<ItemDto>> Create([FromBody] ItemDto entity) => _controller.Create(entity);

        [HttpDelete("{id}")]
        public Task<ActionResult<ItemDto>> Delete(Guid id) => _controller.Delete(id);

        [HttpGet()]
        public Task<ActionResult<IEnumerable<ItemDto>>> GetAll() => _controller.GetAll();

        [HttpGet("{id}")]
        public Task<ActionResult<ItemDto>> GetById(Guid id) => _controller.GetById(id);

        [HttpHead("{id}")]
        public Task<IActionResult> HeadById(Guid id) => _controller.HeadById(id);

        [HttpPut("{id}")]
        public Task<IActionResult> Update(Guid id, [FromBody] ItemDto entity) => _controller.Update(id, entity);
    }
}
