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

        /// <summary>
        /// Gets the history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}/history")]
        public IActionResult GetHistory(Guid id)
        {
            try
            {
                return Ok(_historicalDal.GetHistory(id));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
        }
    }

    public class HistoricalCrudController<TDto, TEntity> :
        Controller
        where TDto : class, IEntity, new()
        where TEntity : class, IEntity, new()
    {
        private readonly HistoricalCrud<TEntity> _dal;
        private readonly IMapper _mapper;

        public HistoricalCrudController(HistoricalCrud<TEntity> dal, IMapper mapper)
        {
            _dal = dal;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var entities = _dal.GetAll();
            var dtos = entities.Select(_ => _mapper.Map<TDto>(_));
            return Ok(dtos);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetById(Guid id)
        {
            try
            {
                return Ok(_mapper.Map<TDto>(_dal.GetById(id)));
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
        public IActionResult Create([FromBody] TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var createdEntity = _dal.Create(entity);
            return Ok(_mapper.Map<TDto>(createdEntity));
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] TDto dto)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(dto);
                _dal.Update(id, entity);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
        }

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

        [HttpPost("{entityId}/restore")]
        public IActionResult Restore(Guid entityId /*, string entityName*/)
        {
            try
            {
                return Ok(_dal.Restore(entityId));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Gets the history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}/history")]
        public IActionResult GetHistory(Guid id)
        {
            try
            {
                return Ok(_dal.GetHistory(id));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
        }
    }
}
