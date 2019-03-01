using LSG.GenericCrud.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Controllers
{
    public interface IHistoricalCrudRestoreController<T> : IHistoricalCrudRestoreController<Guid, T> where T : class, IEntity, new() { }
    public interface IHistoricalCrudRestoreController<T1, T2> : ICrudController<T1, T2> where T2 : class, IEntity<T1>, new()
    {
        Task<IActionResult> RestoreFromDeletedEntity(T1 id); // TODO: Transtype return type to ActionResult<T2>
        Task<ActionResult<T2>> RestoreFromChangeset(T1 id, Guid changesetId);
    }
}
