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
    public class ContributorsController :
        ControllerBase,
        ICrudController<Guid, Contributor>
    {
        private readonly ICrudController<Guid, Contributor> _controller;

        public ContributorsController(ICrudController<Guid, Contributor> controller)
        {
            _controller = controller;
        }

        [HttpPost]
        public Task<ActionResult<Contributor>> Create([FromBody] Contributor entity) => _controller.Create(entity);

        [HttpDelete("{id}")]
        public Task<ActionResult<Contributor>> Delete(Guid id) => _controller.Delete(id);

        [HttpGet()]
        public Task<ActionResult<IEnumerable<Contributor>>> GetAll() => _controller.GetAll();

        [HttpGet("{id}")]
        public Task<ActionResult<Contributor>> GetById(Guid id) => _controller.GetById(id);

        [HttpHead("{id}")]
        public Task<IActionResult> HeadById(Guid id) => _controller.HeadById(id);

        [HttpPut("{id}")]
        public Task<IActionResult> Update(Guid id, [FromBody] Contributor entity) => _controller.Update(id, entity);
    }
}
