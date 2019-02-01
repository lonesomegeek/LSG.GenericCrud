using System;
using System.Threading.Tasks;
using LSG.GenericCrud.Extensions.Services;
using LSG.GenericCrud.Models;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.Extensions.Controllers
{
    public class ReadeableCrudController<T> :
        ControllerBase, IReadeableCrudController<T> where T : class, IEntity, new()
    {
        private readonly IReadeableCrudService<T> _service;

        public ReadeableCrudController(IReadeableCrudService<T> service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<IActionResult> GetAllReadStatus() => Ok(await _service.GetAllAsync());

        [HttpPost]
        [Route("read")]
        public virtual async Task<IActionResult> MarkAllAsRead()
        {
            await _service.MarkAllAsRead();
            return NoContent();
        }

        [HttpPost]
        [Route("read/{id}")]
        public virtual async Task<IActionResult> MarkOneAsRead(Guid id)
        {
            await _service.MarkOneAsRead(id);
            return NoContent();
        }

        [HttpPost]
        [Route("unread")]
        public virtual async Task<IActionResult> MarkAllAsUnread()
        {
            await _service.MarkAllAsUnread();
            return NoContent();
        }
        [HttpPost]
        [Route("unread/{id}")]
        public virtual async Task<IActionResult> MarkOneAsUnread(Guid id)
        {
            await _service.MarkOneAsUnread(id);
            return NoContent();
        }
    }
}
