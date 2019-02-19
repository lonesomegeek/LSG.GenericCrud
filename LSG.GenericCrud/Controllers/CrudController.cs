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
    public class CrudController<T> : 
        ControllerBase, 
        ICrudController<T> 
        where T : class, IEntity, new()
    {
        
        private readonly ICrudController<Guid, T> _controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudAsyncController{T}"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public CrudController(ICrudController<Guid, T> controller)
        {
            _controller = controller;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<T>>> GetAll() => await _controller.GetAll();
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<T>> GetById(Guid id) => await _controller.GetById(id);
        [HttpPost]
        public virtual async Task<ActionResult<T>> Create(T entity) => await _controller.Create(entity);
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(Guid id, T entity) => await _controller.Update(id, entity);
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<T>> Delete(Guid id) => await _controller.Delete(id);
    }

    /// <summary>
    /// Asynchronous Crud Controller endpoints
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class CrudController<T1, T2> :
        ControllerBase,
        ICrudController<T1, T2>
        where T2 : class, IEntity<T1>, new()
    {
        /// <summary>
        /// The service
        /// </summary>
        private readonly ICrudService<T1, T2> _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudAsyncController{T}"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public CrudController(ICrudService<T1, T2> service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<T2>>> GetAll() => Ok(await _service.GetAllAsync());

        /// <summary>
        /// Gets the by identifier if it exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<T2>> GetById(T1 id)
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
        public virtual async Task<ActionResult<T2>> Create([FromBody] T2 entity)
        {
            var createdEntity = await _service.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, createdEntity);
        }


        /// <summary>
        /// Updates the specified identifier if it exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(T1 id, [FromBody] T2 entity)
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
        /// Deletes the specified identifier if it exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<T2>> Delete(T1 id)
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
