using System;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.Controllers
{
    /// <summary>
    /// Crud Controller endpoints
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class CrudController<T> : Controller where T : class, IEntity, new()
    {
        /// <summary>
        /// The service
        /// </summary>
        private readonly ICrudService<T> _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudController{T}"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public CrudController(ICrudService<T> service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual IActionResult GetAll() => Ok(_service.GetAll());

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        public virtual IActionResult GetById(Guid id)
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

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPost("")]
        public virtual IActionResult Create([FromBody] T entity) => Ok(_service.Create(entity));

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public virtual IActionResult Update(Guid id, [FromBody] T entity)
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

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public virtual IActionResult Delete(Guid id)
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

        /// <summary>
        /// Determines if entity with the specified identifier exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpHead("{id}")]
        public virtual IActionResult Head(Guid id)
        {
            if (_service.Head(id)) return Ok();
            else return NotFound();
        }

        [HttpPatch("{id}")]
        public virtual IActionResult Patch(Guid id, [FromBody] JsonPatchDocument<T> patches)
        {
            _service.Patch(id, patches);
        }
    }
}
