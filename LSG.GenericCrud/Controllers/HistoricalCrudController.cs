using System;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.Controllers
{
    /// <summary>
    /// Historical Crud Controller endpoints
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="LSG.GenericCrud.Controllers.CrudController{T}" />
    public class HistoricalCrudController<T> : CrudController<T> where T : class, IEntity, new()
    {
        /// <summary>
        /// The historical crud service
        /// </summary>
        private readonly IHistoricalCrudService<T> _historicalCrudService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoricalCrudController{T}"/> class.
        /// </summary>
        /// <param name="historicalCrudService">The historical crud service.</param>
        public HistoricalCrudController(IHistoricalCrudService<T> historicalCrudService) : base(historicalCrudService)
        {
            _historicalCrudService = historicalCrudService;
        }

        /// <summary>
        /// Gets the history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}/history")]
        public IActionResult GetHistory(Guid id)
        {
            try
            {
                return Ok(_historicalCrudService.GetHistory(id));
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
        public IActionResult Restore(Guid id)
        {
            try
            {
                return Ok(_historicalCrudService.Restore(id));
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
