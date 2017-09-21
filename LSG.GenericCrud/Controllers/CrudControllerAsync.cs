using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.Controllers
{
    public class CrudControllerAsync<T> : Controller where T : class, IEntity, new()
    {
        /// <summary>
        /// The _service
        /// </summary>
        protected readonly ICrud<T> _dal;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericCrudApiController{T, TEntity}"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public CrudControllerAsync(ICrud<T> dal)
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
        public async Task<IActionResult> GetById(Guid id) => Ok(await _dal.GetByIdAsync(id));

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
        public IActionResult Update(Guid id, [FromBody] T entity) => throw new NotImplementedException();

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="WebRequestMethods.Http.HttpResponseException"></exception>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id) => throw new NotImplementedException();
    }
}
