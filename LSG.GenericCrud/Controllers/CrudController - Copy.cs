using System;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.Controllers
{
    /// <summary>
    /// Crud Controller endpoints
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class CrudController<TEntityType, TEntityIdType> : Controller 
        where TEntityType : class, IEntity<TEntityIdType>, new()
        
    {
        /// <summary>
        /// The service
        /// </summary>
        private readonly ICrudService<TEntityType> _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudController{T}"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public CrudController(ICrudService<TEntityType> service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetById(TEntityIdType id)
        {
            try
            {
                return Ok(_service.GetById<TEntityType, TEntityIdType>(id));
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
        /// <returns></returns>
        [HttpPost("")]
        public IActionResult Create([FromBody]TEntityType entity) => Ok(_service.Create(entity));

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] TEntityType entity)
        {
            try
            {
                return Ok(_service.Update(id, entity));
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
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                return Ok(_service.Delete(id));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
        }
    }
}
