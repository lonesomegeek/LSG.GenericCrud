using LSG.GenericCrud.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Controllers
{   public interface IHistoricalCrudDeltaController<T1, T2> : ICrudController<T1, T2> where T2 : class, IEntity<T1>, new()
    {
        Task<IActionResult> Delta(T1 id, [FromBody] DeltaRequest request); // TODO: RETURN OBJECT
    }
}
