using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Samples.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Samples.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController :
        ControllerBase,
        ICrudController<Guid, User>
    {
        private readonly ICrudController<Guid, User> _controller;

        public UsersController(ICrudController<Guid, User> controller)
        {
            _controller = controller;
        }

        [HttpPost]
        public Task<ActionResult<User>> Create([FromBody] User entity) => _controller.Create(entity);

        [HttpDelete("{id}")]
        public Task<ActionResult<User>> Delete(Guid id) => _controller.Delete(id);

        [HttpGet()]
        public Task<ActionResult<IEnumerable<User>>> GetAll() => _controller.GetAll();

        [HttpGet("{id}")]
        public Task<ActionResult<User>> GetById(Guid id) => _controller.GetById(id);

        [HttpHead("{id}")]
        public Task<IActionResult> HeadById(Guid id) => _controller.HeadById(id);

        [HttpPut("{id}")]
        public Task<IActionResult> Update(Guid id, [FromBody] User entity) => _controller.Update(id, entity);
    }
}
