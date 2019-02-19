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
    /// Asynchronous Historical Crud Controller endpoints
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="LSG.GenericCrud.Controllers.CrudAsyncController{T}" />
    public class HistoricalCrudController<T> : 
        ControllerBase,
        IHistoricalCrudController<T> where T : class, IEntity, new()
    {
        private readonly IHistoricalCrudController<Guid, T> _controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoricalCrudAsyncController{T}"/> class.
        /// </summary>
        /// <param name="historicalCrudService">The historical crud service.</param>
        public HistoricalCrudController(IHistoricalCrudController<Guid, T> controller)
        {
            _controller = controller;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<T>>> GetAll() => await _controller.GetAll();
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<T>> GetById(Guid id) => await _controller.GetById(id);
        [HttpHead("{id}")]
        public virtual async Task<IActionResult> HeadById(Guid id) => await _controller.HeadById(id);
        [HttpPost]
        public virtual async Task<ActionResult<T>> Create(T entity) => await _controller.Create(entity);
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(Guid id, T entity) => await _controller.Update(id, entity);
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<T>> Delete(Guid id) => await _controller.Delete(id);
        [HttpGet("{id}/history")]
        public virtual async Task<IActionResult> GetHistory(Guid id) => await _controller.GetHistory(id);
        [HttpPost("{id}/restore")]
        public virtual async Task<IActionResult> Restore(Guid id) => await _controller.Restore(id);
    }

    /// <summary>
    /// Asynchronous Historical Crud Controller endpoints
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="LSG.GenericCrud.Controllers.CrudAsyncController{T}" />
    public class HistoricalCrudController<T1, T2> :
        ControllerBase,
        IHistoricalCrudController<T1, T2> where T2 : class, IEntity<T1>, new()
    {
        private readonly ICrudController<T1, T2> _crudController;

        /// <summary>
        /// The historical crud service
        /// </summary>
        private readonly IHistoricalCrudService<T1, T2> _historicalCrudService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoricalCrudAsyncController{T}"/> class.
        /// </summary>
        /// <param name="historicalCrudService">The historical crud service.</param>
        public HistoricalCrudController(ICrudController<T1, T2> crudController, IHistoricalCrudService<T1, T2> historicalCrudService)
        {
            _crudController = crudController;
            _historicalCrudService = historicalCrudService;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<T2>>> GetAll() => await _crudController.GetAll();
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<T2>> GetById(T1 id) => await _crudController.GetById(id);
        [HttpHead("{id}")]
        public virtual async Task<IActionResult> HeadById(T1 id) => await _crudController.HeadById(id);
        /// <summary>
        /// Gets the history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}/history")]
        public virtual async Task<IActionResult> GetHistory(T1 id)
        {
            try
            {
                return Ok(await _historicalCrudService.GetHistoryAsync(id));
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Restores the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPost("{id}/restore")]
        public virtual async Task<IActionResult> Restore(T1 id)
        {
            try
            {
                return Ok(await _historicalCrudService.RestoreAsync(id));
            }
            catch (EntityNotFoundException)
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
            var createdEntity = await _historicalCrudService.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, createdEntity);
        }


        /// <summary>
        /// Updates the specified identifier.
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
                await _historicalCrudService.UpdateAsync(id, entity);

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
        public virtual async Task<ActionResult<T2>> Delete(T1 id)
        {
            try
            {
                return Ok(await _historicalCrudService.DeleteAsync(id));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
        }
    }
}
