using System;
using System.Threading.Tasks;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.Controllers
{
    public class CrudAsyncController<T> : Controller where T : class, IEntity, new()
    {
        /// <summary>
        /// The _service
        /// </summary>
        protected readonly ICrud<T> _dal;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericCrudApiController{T, TEntity}"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public CrudAsyncController(ICrud<T> dal)
        {
            _dal = dal;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _dal.GetAllAsync());


        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await _dal.GetByIdAsync(id));
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
        public async Task<IActionResult> Create([FromBody] T entity) => Ok(await _dal.CreateAsync(entity));

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <exception cref="WebRequestMethods.Http.HttpResponseException"></exception>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] T entity)
        {
            try
            {
                await _dal.UpdateAsync(id, entity);
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
        /// <exception cref="WebRequestMethods.Http.HttpResponseException"></exception>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _dal.DeleteAsync(id);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }

        }
    }
}
