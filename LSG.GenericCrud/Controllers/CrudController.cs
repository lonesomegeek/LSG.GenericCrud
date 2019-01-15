using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.Controllers
{
    /// <summary>
    /// Asynchronous Crud Controller endpoints
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class CrudController<T> : ControllerBase, ICrudController<T> where T : class, IEntity, new()
    {
        /// <summary>
        /// The service
        /// </summary>
        private readonly ICrudService<T> _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudAsyncController{T}"/> class.
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
        public virtual async Task<ActionResult<IEnumerable<T>>> GetAll() => Ok(await _service.GetAllAsync());

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        public virtual async Task<ActionResult<T>> GetById(Guid id)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id));
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
        [HttpPost]
        public virtual async Task<ActionResult<T>> Create([FromBody] T entity)
        {
            var createdEntity = await _service.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, createdEntity);
        }
        

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(Guid id, [FromBody] T entity)
        {
            // TODO: Add an null id detection
            try
            {
                await _service.UpdateAsync(id, entity);

                return NoContent();
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
        public virtual async Task<ActionResult<T>> Delete(Guid id)
        {
            try
            {
                return Ok(await _service.DeleteAsync(id));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
        }
    }
}
