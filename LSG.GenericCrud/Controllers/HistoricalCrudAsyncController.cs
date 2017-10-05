using System;
using System.Threading.Tasks;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="LSG.GenericCrud.Controllers.CrudController{T}" />
    public class HistoricalCrudAsyncController<T> : CrudAsyncController<T> where T : class, IEntity, new()
    {
        /// <summary>
        /// The historical dal
        /// </summary>
        private readonly HistoricalCrud<T> _dal;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:LSG.GenericCrud.Controllers.HistoricalCrudController`1" /> class.
        /// </summary>
        /// <param name="dal">The dal.</param>
        public HistoricalCrudAsyncController(HistoricalCrud<T> dal) : base(dal)
        {
            _dal = dal;
        }

        /// <summary>
        /// Restores the specified entity identifier.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        /// <returns></returns>
        [HttpPost("{entityId}/restore")]
        public async Task<IActionResult> Restore(Guid entityId /*, string entityName*/)
        {
            try
            {
                return Ok(await _dal.RestoreAsync(entityId));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Gets the history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetHistory(Guid id)
        {
            try
            {
                return Ok(await _dal.GetHistoryAsync(id));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
        }
    }
}
