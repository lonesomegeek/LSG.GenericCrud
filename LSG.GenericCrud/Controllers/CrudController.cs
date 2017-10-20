using System;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.Controllers
{
    public class CrudController<T> : Controller where T : class, IEntity, new()
    {
        private readonly ICrudService<T> _service;

        public CrudController(ICrudService<T> service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetById(Guid id)
        {
            try
            {
                return Ok(_service.GetById(id));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] T entity) => Ok(_service.Create(entity));

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] T entity)
        {
            try
            {
                return Ok(_service.Update(id, entity));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                return Ok(_service.Delete(id));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
        }
    }
}
