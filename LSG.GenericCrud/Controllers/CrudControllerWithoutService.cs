using System;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.Controllers
{
    public class CrudControllerWithoutService<T> : Controller where T : class, IEntity, new()
    {
        /// <summary>
        /// The _service
        /// </summary>
        protected readonly ICrud<T> _dal;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericCrudApiController{T, TEntity}"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public CrudControllerWithoutService(ICrud<T> dal)
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
        public IActionResult GetById(Guid id)
        {
            try
            {
                return Ok(_dal.GetById(id));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        [HttpPost("")]
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
            try
            {
                _dal.Update(id, entity);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="System.Web.Http.HttpResponseException"></exception>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _dal.Delete(id);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }

        }
    }
}
