using LSG.GenericCrud.Models;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.Controllers
{
    public class CrudController<T> : Controller where T : class, IEntity, new()
    {
        private readonly ICrudService<T> _service;

        public CrudController(ICrudService<T> service)
        {
            _service = service;
        }

        public IActionResult GetAll() => Ok(_service.GetAll());
    }
}
