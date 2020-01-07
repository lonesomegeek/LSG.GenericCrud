using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.Controllers
{
    public interface IHistoricalCrudController<T1, T2> : ICrudController<T1, T2> where T2 : class, IEntity<T1>, new()
    {
        Task<IActionResult> GetHistory(T1 id);
    }
}
