using LSG.GenericCrud.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Controllers
{   public interface IHistoricalCrudCopyController<T1, T2> : ICrudController<T1, T2> where T2 : class, IEntity<T1>, new()
    {
        Task<ActionResult<T2>> Copy(T1 id);
        Task<ActionResult<T2>> CopyFromChangeset(T1 entityId, Guid changesetId);
    }
}
