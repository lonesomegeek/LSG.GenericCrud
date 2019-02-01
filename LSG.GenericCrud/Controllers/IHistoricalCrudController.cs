using System;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.Controllers
{
    public interface IHistoricalCrudController<T> : ICrudController<T> where T : class, IEntity, new()
    {
        /// <summary>
        /// Gets the history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<IActionResult> GetHistory(Guid id);

        /// <summary>
        /// Restores the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<IActionResult> Restore(Guid id);
    }
}