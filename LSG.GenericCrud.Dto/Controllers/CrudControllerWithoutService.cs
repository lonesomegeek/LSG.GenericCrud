using System;
using System.Linq;
using AutoMapper;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.Dto.Controllers
{
    //public class CrudControllerWithoutService<TDto, TEntity> : 
    //    Controller
    //    where TDto : class, IEntity, new()
    //    where TEntity : class, IEntity, new()
    //{
    //    private readonly IMapper _mapper;
    //    private readonly Crud<TEntity> _dal;

    //    public CrudControllerWithoutService(Crud<TEntity> dal, IMapper mapper)
    //    {
    //        _dal = dal;
    //        _mapper = mapper;
    //    }

    //    [HttpGet]
    //    public new IActionResult GetAll()
    //    {
    //        var entities = _dal.GetAll();
    //        var dtos = entities.Select(_ => _mapper.Map<TDto>(_));
    //        return Ok(dtos);
    //    }

    //    [Route("{id}")]
    //    [HttpGet]
    //    public IActionResult GetById(Guid id)
    //    {
    //        try
    //        {
    //            return Ok(_mapper.Map<TDto>(_dal.GetById(id)));
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
    //    public IActionResult Create([FromBody] TDto dto)
    //    {
    //        var entity = _mapper.Map<TEntity>(dto);
    //        var createdEntity = _dal.Create(entity);
    //        return Ok(_mapper.Map<TDto>(createdEntity));
    //    }

    //    [HttpPut("{id}")]
    //    public IActionResult Update(Guid id, [FromBody] TDto dto)
    //    {
    //        try
    //        {
    //            var entity = _mapper.Map<TEntity>(dto);
    //            _dal.Update(id, entity);
    //            return Ok();
    //        }
    //        catch (EntityNotFoundException ex)
    //        {
    //            return NotFound();
    //        }
    //    }

    //    [HttpDelete("{id}")]
    //    public IActionResult Delete(Guid id)
    //    {
    //        try
    //        {
    //            _dal.Delete(id);
    //            return Ok();
    //        }
    //        catch (EntityNotFoundException ex)
    //        {
    //            return NotFound();
    //        }

    //    }

    //}
}
