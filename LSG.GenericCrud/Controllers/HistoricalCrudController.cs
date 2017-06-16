using System;
using System.Linq;
using AutoMapper;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LSG.GenericCrud.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="LSG.GenericCrud.Controllers.CrudController{T}" />
    public class HistoricalCrudController<T> : CrudController<T> where T : class, IEntity, new()
    {
        /// <summary>
        /// The historical dal
        /// </summary>
        private readonly HistoricalCrud<T> _historicalDal;

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoricalCrudController{T}"/> class.
        /// </summary>
        /// <param name="dal">The dal.</param>
        public HistoricalCrudController(HistoricalCrud<T> dal) : base(dal)
        {
            _historicalDal = dal;
        }

        /// <summary>
        /// Restores the specified entity identifier.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        /// <returns></returns>
        [HttpPost("{entityId}/restore")]
        public IActionResult Restore(Guid entityId /*, string entityName*/)
        {
            try
            {
                return Ok(_historicalDal.Restore(entityId));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
        }
    }

    public class HistoricalCrudController<TDto, TEntity> :
        CrudController<TDto, TEntity>
        where TDto : class, IEntity, new()
        where TEntity : class, IEntity, new()
    {
        private readonly HistoricalCrud<TEntity> _historicalDal;

        public HistoricalCrudController(HistoricalCrud<TEntity> dal, IMapper mapper) : base(dal, mapper)
        {
            _historicalDal = dal;
        }

        [HttpPost("{entityId}/restore")]
        public IActionResult Restore(Guid entityId /*, string entityName*/)
        {
            try
            {
                return Ok(_historicalDal.Restore(entityId));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
        }
    }
}
