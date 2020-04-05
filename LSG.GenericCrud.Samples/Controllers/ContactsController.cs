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
    public class ContactsController :
        ControllerBase,
        ICrudController<Guid, Contact>
    {
        private readonly ICrudController<Guid, Contact> _controller;

        public ContactsController(ICrudController<Guid, Contact> controller)
        {
            _controller = controller;
        }

        [HttpPost]
        public Task<ActionResult<Contact>> Create([FromBody] Contact entity) => _controller.Create(entity);

        [HttpDelete("{id}")]
        public Task<ActionResult<Contact>> Delete(Guid id) => _controller.Delete(id);

        [HttpGet()]
        public Task<ActionResult<IEnumerable<Contact>>> GetAll() => _controller.GetAll();

        [HttpGet("{id}")]
        public Task<ActionResult<Contact>> GetById(Guid id) => _controller.GetById(id);

        [HttpHead("{id}")]
        public Task<IActionResult> HeadById(Guid id) => _controller.HeadById(id);

        [HttpPut("{id}")]
        public Task<IActionResult> Update(Guid id, [FromBody] Contact entity) => _controller.Update(id, entity);
    }
}
