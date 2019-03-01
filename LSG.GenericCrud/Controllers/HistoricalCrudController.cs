using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Helpers;
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
        IHistoricalCrudController<T>,
        IHistoricalCrudCopyController<T>,
        IHistoricalCrudDeltaController<T>,
        IHistoricalCrudReadStatusController<T>,
        IHistoricalCrudRestoreController<T>
        where T : class, IEntity, new()
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
        [HttpPost("{id}/copy")]
        public virtual async Task<ActionResult<T>> Copy(Guid id) => await ((IHistoricalCrudCopyController<T>)_controller).Copy(id);
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(Guid id, T entity) => await _controller.Update(id, entity);
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<T>> Delete(Guid id) => await _controller.Delete(id);
        [HttpGet("{id}/history")]
        public virtual async Task<IActionResult> GetHistory(Guid id) => await _controller.GetHistory(id);
        [HttpPost("{id}/restore")]
        public virtual async Task<IActionResult> RestoreFromDeletedEntity(Guid id) => await ((IHistoricalCrudRestoreController<T>)_controller).RestoreFromDeletedEntity(id);
        [HttpPost("{entityId}/restore/{changesetId}")]
        public virtual async Task<ActionResult<T>> RestoreFromChangeset(Guid entityId, Guid changesetId) => await ((IHistoricalCrudRestoreController<T>)_controller).RestoreFromChangeset(entityId, changesetId);
        [HttpPost("{entityId}/copy/{changesetId}")]
        public virtual async Task<ActionResult<T>> CopyFromChangeset(Guid entityId, Guid changesetId) => await ((IHistoricalCrudCopyController<T>)_controller).CopyFromChangeset(entityId, changesetId);
        [HttpPost("read")]
        public virtual async Task<IActionResult> MarkAllAsRead() => await ((IHistoricalCrudReadStatusController<T>)_controller).MarkAllAsRead();
        [HttpPost("unread")]
        public virtual async Task<IActionResult> MarkAllAsUnread() => await ((IHistoricalCrudReadStatusController<T>)_controller).MarkAllAsUnread();
        [HttpPost("{id}/read")]
        public virtual async Task<IActionResult> MarkOneAsRead(Guid id) => await ((IHistoricalCrudReadStatusController<T>)_controller).MarkOneAsRead(id);
        [HttpPost("{id}/unread")]
        public virtual async Task<IActionResult> MarkOneAsUnread(Guid id) => await ((IHistoricalCrudReadStatusController<T>)_controller).MarkOneAsUnread(id);
        [HttpGet("read-status")]
        public virtual async Task<ActionResult<IEnumerable<ReadeableStatus<T>>>> GetReadStatus() => await ((IHistoricalCrudReadStatusController<T>)_controller).GetReadStatus();
        [HttpGet("{id}/read-status")]
        public virtual async Task<ActionResult<ReadeableStatus<T>>> GetReadStatusById(Guid id) => await ((IHistoricalCrudReadStatusController<T>)_controller).GetReadStatusById(id);
        [HttpPost("{id}/delta")]
        public virtual async Task<IActionResult> Delta(Guid id, DeltaRequest request) => await ((IHistoricalCrudDeltaController<T>)_controller).Delta(id, request);
    }

    /// <summary>
    /// Asynchronous Historical Crud Controller endpoints
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="LSG.GenericCrud.Controllers.CrudAsyncController{T}" />
    public class HistoricalCrudController<T1, T2> :
        ControllerBase,
        IHistoricalCrudController<T1, T2>,
        IHistoricalCrudCopyController<T1, T2>,
        IHistoricalCrudDeltaController<T1, T2>,
        IHistoricalCrudReadStatusController<T1, T2>,
        IHistoricalCrudRestoreController<T1, T2> where T2 : class, IEntity<T1>, new()
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
        public virtual async Task<IActionResult> RestoreFromDeletedEntity(T1 id)
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

        [HttpPost("{entityId}/copy/{changesetId}")]
        public virtual async Task<ActionResult<T2>> CopyFromChangeset(T1 entityId, Guid changesetId)
        {
            try
            {
                var createdEntity = await _historicalCrudService.CopyFromChangeset(entityId, changesetId);
                return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, createdEntity);
            }
            catch (Exception ex)
            {
                if (ex is EntityNotFoundException) return NotFound($"Entity not found with id: {entityId}");
                if (ex is ChangesetNotFoundException) return NotFound($"Changeset not found with id: {changesetId}");
                throw;
            }
        }

        [HttpPost("{entityId}/restore/{changesetId}")]
        public virtual async Task<ActionResult<T2>> RestoreFromChangeset(T1 entityId, Guid changesetId)
        {
            try
            {
                await _historicalCrudService.RestoreFromChangeset(entityId, changesetId);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex is EntityNotFoundException) return NotFound($"Entity not found with id: {entityId}");
                if (ex is ChangesetNotFoundException) return NotFound($"Changeset not found with id: {changesetId}");
                throw;
            }
        }

        [HttpPost("read")]
        public virtual async Task<IActionResult> MarkAllAsRead()
        {
            await _historicalCrudService.MarkAllAsRead();
            return NoContent();
        }
        [HttpPost("unread")]
        public virtual async Task<IActionResult> MarkAllAsUnread()
        {
            await _historicalCrudService.MarkAllAsUnread();
            return NoContent();
        }
        [HttpPost("{id}/read")]
        public virtual async Task<IActionResult> MarkOneAsRead(T1 id)
        {
            await _historicalCrudService.MarkOneAsRead(id);
            return NoContent();
        }
        [HttpPost("{id}/unread")]
        public virtual async Task<IActionResult> MarkOneAsUnread(T1 id)
        {
            await _historicalCrudService.MarkOneAsUnread(id);
            return NoContent();
        }

        [HttpGet("read-status")]
        public virtual async Task<ActionResult<IEnumerable<ReadeableStatus<T2>>>> GetReadStatus() => Ok(await _historicalCrudService.GetReadStatusAsync());


        [HttpGet("{id}/read-status")]
        public virtual async Task<ActionResult<ReadeableStatus<T2>>> GetReadStatusById(T1 id) => Ok(await _historicalCrudService.GetReadStatusByIdAsync(id));
        

        [HttpPost("{id}/delta")]
        public virtual async Task<IActionResult> Delta(T1 id, DeltaRequest request) => Ok(await _historicalCrudService.Delta(id, request));

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

        [HttpPost("{id}/copy")]
        public virtual async Task<ActionResult<T2>> Copy(T1 id)
        {
            var createdEntity = await _historicalCrudService.CopyAsync(id);
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
