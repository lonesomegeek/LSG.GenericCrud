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
    public class CarsController :
        ControllerBase,
        ICrudController<Car>
    {
        private readonly ICrudController<Car> _controller;

        public CarsController(ICrudController<Car> controller)
        {
            _controller = controller;
        }

        [HttpPost]
        public async Task<ActionResult<Car>> Create([FromBody] Car entity) => await _controller.Create(entity);
        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> Delete(Guid id) => await _controller.Delete(id);
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetAll() => await _controller.GetAll();
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<Car>> GetById(Guid id) => await _controller.GetById(id);
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Car entity) => await _controller.Update(id, entity);
    }
}
