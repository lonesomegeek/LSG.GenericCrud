using System;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        [HttpPost]
        public IActionResult Create([FromBody] T entity) => Ok(_dal.Create(entity));

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <exception cref="System.Web.Http.HttpResponseException"></exception>
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] T entity)
        {
            _dal.Update(id, entity);
            return Ok();
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="System.Web.Http.HttpResponseException"></exception>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _dal.Delete(id);
            return Ok();
        }
    }
}
