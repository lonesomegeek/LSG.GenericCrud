using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Controllers;
using Microsoft.AspNetCore.Mvc;
using Sample.App.Api.Models;

namespace Sample.App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController :
        ControllerBase,
        ICrudController<Friend>
    {
        private readonly ICrudController<Friend> _controller;

        public FriendsController(ICrudController<Friend> controller)
        {
            _controller = controller;
        }

        [HttpPost]
        public async Task<ActionResult<Friend>> Create([FromBody] Friend entity) => await _controller.Create(entity);
        [HttpDelete("{id}")]
        public async Task<ActionResult<Friend>> Delete(Guid id) => await _controller.Delete(id);
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Friend>>> GetAll() => await _controller.GetAll();
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<Friend>> GetById(Guid id) => await _controller.GetById(id);
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Friend entity) => await _controller.Update(id, entity);
    }
}
