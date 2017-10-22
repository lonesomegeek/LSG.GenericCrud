using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;

namespace LSG.GenericCrud.Dto.Services
{
    public class CrudService<TDto, TEntity> : CrudService<TEntity> 
        where TDto : IEntity 
        where TEntity : IEntity
    {
        private readonly IMapper _mapper;

        public CrudService(ICrudRepository<TEntity> repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        public TDto Create(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var createdEntity = base.Create(entity);
            return _mapper.Map<TDto>(createdEntity);
        }
    }
}
