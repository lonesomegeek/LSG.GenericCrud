using System;
using System.Linq;
using AutoMapper;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GeneriCrud.Dto.Controllers
{
    public class CrudController<T> : Controller where T : class, IEntity, new()
    {
        /// <summary>
        /// The _service
        /// </summary>
        protected readonly ICrud<T> _dal;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericCrudApiController{T, TEntity}"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public CrudController(ICrud<T> dal)
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

    public class CrudController<TDto, TEntity> : 
        Controller
        where TDto : class, IEntity, new()
        where TEntity : class, IEntity, new()
    {
        private readonly IMapper _mapper;
        private readonly Crud<TEntity> _dal;

        public CrudController(Crud<TEntity> dal, IMapper mapper)
        {
            _dal = dal;
            _mapper = mapper;
        }

        [HttpGet]
        public new IActionResult GetAll()
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

    }
}
