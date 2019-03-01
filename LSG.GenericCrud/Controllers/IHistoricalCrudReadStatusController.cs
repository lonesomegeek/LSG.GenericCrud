using LSG.GenericCrud.Models;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Controllers
{
    public interface IHistoricalCrudReadStatusController<T> : IHistoricalCrudReadStatusController<Guid, T> where T : class, IEntity, new() { }
    public interface IHistoricalCrudReadStatusController<T1, T2> : ICrudController<T1, T2> where T2 : class, IEntity<T1>, new()
    {
        Task<IActionResult> MarkAllAsRead();
        Task<IActionResult> MarkAllAsUnread();
        Task<IActionResult> MarkOneAsRead(T1 id);
        Task<IActionResult> MarkOneAsUnread(T1 id);
        Task<ActionResult<IEnumerable<ReadeableStatus<T2>>>> GetReadStatus();
        Task<ActionResult<ReadeableStatus<T2>>> GetReadStatusById(T1 id);
    }
}
