using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Samples.Models;
using LSG.GenericCrud.Services;
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
            ICrudController<Guid, Item> {
        private readonly ICrudController<Guid, Item> _controller;

        public ItemsController(ICrudController<Guid, Item> controller)
        {
            _controller = controller;
        }

        [HttpPost]
        public Task<ActionResult<Item>> Create([FromBody] Item entity) => _controller.Create(entity);

        [HttpDelete("{id}")]
        public Task<ActionResult<Item>> Delete(Guid id) => _controller.Delete(id);

        [HttpGet()]
        public Task<ActionResult<IEnumerable<Item>>> GetAll() => _controller.GetAll();

        [HttpGet("{id}")]
        public Task<ActionResult<Item>> GetById(Guid id) => _controller.GetById(id);

        [HttpHead("{id}")]
        public Task<IActionResult> HeadById(Guid id) => _controller.HeadById(id);

        [HttpPut("{id}")]
        public Task<IActionResult> Update(Guid id, [FromBody] Item entity) => _controller.Update(id, entity);
    }
}
