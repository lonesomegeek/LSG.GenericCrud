using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.Controllers
{
    public interface ICrudController<T> where T : class, IEntity, new()
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        Task<ActionResult<IEnumerable<T>>> GetAll();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<ActionResult<T>> GetById(Guid id);

        /// <summary>
        /// Head for a specific object
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns 204 (No Content) if entity exists, 404 (NotFound) otherwise</returns>
        Task<IActionResult> HeadById(Guid id);

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<ActionResult<T>> Create([FromBody] T entity);

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<IActionResult> Update(Guid id, [FromBody] T entity);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<ActionResult<T>> Delete(Guid id);
    }
}