using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.Controllers
{
    public interface IHistoricalCrudController<T> : IHistoricalCrudController<Guid, T> where T : class, IEntity, new() { }

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

        Task<ActionResult<T2>> CopyFromChangeset(T1 entityId, Guid changesetId);

        Task<IActionResult> MarkAllAsRead();

        Task<IActionResult> MarkAllAsUnread();

        Task<IActionResult> MarkOneAsRead(T1 id);

        Task<IActionResult> MarkOneAsUnread(T1 id);

        Task<ActionResult<IEnumerable<ReadeableStatus<T2>>>> GetReadStatus();

        Task<ActionResult<ReadeableStatus<T2>>> GetReadStatusById(T1 id);

        Task<IActionResult> Delta(T1 id, [FromBody] DeltaRequest request);

    }
}
