using System;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.Controllers
{
    public interface IHistoricalCrudController<T> : ICrudController<Guid, T> where T : class, IEntity, new() { }

    public interface IHistoricalCrudController<T1, T2> : ICrudController<T1, T2> where T2 : class, IEntity<T1>, new()
    {
        /// <summary>
        /// Gets the history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<IActionResult> GetHistory(T1 id);

        /// <summary>
        /// Restores the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<IActionResult> Restore(T1 id);
    }
}