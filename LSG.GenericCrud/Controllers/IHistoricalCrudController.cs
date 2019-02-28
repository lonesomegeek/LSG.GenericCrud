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
        Task<IActionResult> GetHistory(T1 id);

        // TODO: Place in IHistoricalCrudRestoreController
        Task<IActionResult> RestoreFromDeletedEntity(T1 id); // TODO: Transtype return type to ActionResult<T2>
        Task<ActionResult<T2>> RestoreFromChangeset(T1 id, Guid changesetId);
        // TODO: Place in IHistoricalCrudCopyController
        Task<ActionResult<T2>> Copy(T1 id);
        Task<ActionResult<T2>> CopyFromChangeset(T1 entityId, Guid changesetId);

        // TODO: Place in IHistoricalCrudReadController
        Task<IActionResult> MarkAllAsRead();
        Task<IActionResult> MarkAllAsUnread();
        Task<IActionResult> MarkOneAsRead(T1 id);
        Task<IActionResult> MarkOneAsUnread(T1 id);
        Task<ActionResult<IEnumerable<ReadeableStatus<T2>>>> GetReadStatus();
        Task<ActionResult<ReadeableStatus<T2>>> GetReadStatusById(T1 id);

        // TODO : Place in IHistoricalCrudDeltaController
        Task<IActionResult> Delta(T1 id, [FromBody] DeltaRequest request);

    }
}
