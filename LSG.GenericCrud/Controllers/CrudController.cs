using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LSG.GenericCrud.Controllers
{
    public class CrudController<T> : Controller where T : class, IEntity, new()
    {
        /// <summary>
        /// The _service
        /// </summary>
        private readonly Crud<T> _dal;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericCrudApiController{T, TEntity}"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public CrudController(Crud<T> dal)
        {
            _dal = dal;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll() => Ok(_dal.GetAll());

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetById(Guid id) => Ok(_dal.GetById(id));
    }
}
