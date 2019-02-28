using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.Controllers
{
    public interface ICrudController<T> : ICrudController<Guid, T> where T : class, IEntity<Guid>, new() { }

    public interface ICrudController<T1, T2> where T2 : class, IEntity<T1>, new()
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        Task<ActionResult<IEnumerable<T2>>> GetAll();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<ActionResult<T2>> GetById(T1 id);

        Task<IActionResult> HeadById(T1 id);

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<ActionResult<T2>> Create([FromBody] T2 entity);

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<ActionResult<T2>> Copy(T1 id); // TODO: Place in ICrudCopyController

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<IActionResult> Update(T1 id, [FromBody] T2 entity);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<ActionResult<T2>> Delete(T1 id);
    }
}