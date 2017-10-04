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

    //public class CrudAsyncController<TDto, TEntity> :
    //  Controller
    //  where TDto : class, IEntity, new()
    //  where TEntity : class, IEntity, new()
    //{
    //    private readonly IMapper _mapper;
    //    private readonly Crud<TEntity> _dal;

    //    public CrudAsyncController(Crud<TEntity> dal, IMapper mapper)
    //    {
    //        _dal = dal;
    //        _mapper = mapper;
    //    }

    //    [HttpGet]
    //    public new async Task<IActionResult> GetAll()
    //    {
    //        var entities = await _dal.GetAllAsync();
    //        var dtos = entities.Select(_ => _mapper.Map<TDto>(_));
    //        return Ok(dtos);
    //    }

    //    [Route("{id}")]
    //    [HttpGet]
    //    public async Task<IActionResult> GetById(Guid id)
    //    {
    //        try
    //        {
    //            return Ok(_mapper.Map<TDto>(await _dal.GetByIdAsync(id)));
    //        }
    //        catch (EntityNotFoundException ex)
    //        {
    //            return NotFound();
    //        }
    //    }

    //    /// <summary>
    //    /// Creates the specified entity.
    //    /// </summary>
    //    /// <param name="entity">The entity.</param>
    //    [HttpPost("")]
    //    public async Task<IActionResult> Create([FromBody] TDto dto)
    //    {
    //        var entity = _mapper.Map<TEntity>(dto);
    //        var createdEntity = await _dal.CreateAsync(entity);
    //        return Ok(_mapper.Map<TDto>(createdEntity));
    //    }

    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> Update(Guid id, [FromBody] TDto dto)
    //    {
    //        try
    //        {
    //            var entity = _mapper.Map<TEntity>(dto);
    //            await _dal.UpdateAsync(id, entity);
    //            return Ok();
    //        }
    //        catch (EntityNotFoundException ex)
    //        {
    //            return NotFound();
    //        }
    //    }

    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> Delete(Guid id)
    //    {
    //        try
    //        {
    //            await _dal.DeleteAsync(id);
    //            return Ok();
    //        }
    //        catch (EntityNotFoundException ex)
    //        {
    //            return NotFound();
    //        }

    //    }

    //}
}
