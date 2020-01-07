using LSG.GenericCrud.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Controllers
{
    public interface ICrudCopyController<T1, T2> where T2 : class, IEntity<T1>, new()
    {
        Task<ActionResult<T2>> Copy(T1 id);
    }
}
