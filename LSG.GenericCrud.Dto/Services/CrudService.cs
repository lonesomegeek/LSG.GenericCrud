using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;

namespace LSG.GenericCrud.Dto.Services
{
    public class CrudService<TDto, TEntity> : CrudService<TEntity>, ICrudService<TDto>
        where TDto : IEntity 
        where TEntity : IEntity
    {
        private readonly IMapper _mapper;

        public CrudService(ICrudRepository<TEntity> repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        public new IEnumerable<TDto> GetAll()
        {
            return base.GetAll().Select(_ => _mapper.Map<TDto>(_));
        }
        public new async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var results = await base.GetAllAsync();
            return results.Select(_ => _mapper.Map<TDto>(_));
        }

        public new TDto GetById(Guid id)
        {
            return _mapper.Map<TDto>(base.GetById(id));
        }


        public new async Task<TDto> GetByIdAsync(Guid id)
        {
            var result = await base.GetByIdAsync(id);
            return _mapper.Map<TDto>(result);
        }

        public virtual TDto Create(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var createdEntity = base.Create(entity);
            return _mapper.Map<TDto>(createdEntity);
        }

        public async Task<TDto> CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var createdEntity = await base.CreateAsync(entity);
            return _mapper.Map<TDto>(createdEntity);
        }

        public TDto Update(Guid id, TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var updatedEntity = base.Update(id, entity);
            return _mapper.Map<TDto>(updatedEntity);
        }

        public async Task<TDto> UpdateAsync(Guid id, TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var updatedEntity = await base.UpdateAsync(id, entity);
            return _mapper.Map<TDto>(updatedEntity);
        }

        public new TDto Delete(Guid id)
        {
            var deletedEntity = base.Delete(id);
            return _mapper.Map<TDto>(deletedEntity);
        }
        
        public new async Task<TDto> DeleteAsync(Guid id)
        {
            var deletedEntity = await base.DeleteAsync(id);
            return _mapper.Map<TDto>(deletedEntity);
        }
    }
}
