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
    public class HistoricalCrudService<TDto, TEntity> : HistoricalCrudService<TEntity>, IHistoricalCrudService<TDto>
        where TDto : IEntity
        where TEntity : IEntity, new()
    {
        private readonly IMapper _mapper;

        public HistoricalCrudService(ICrudRepository<TEntity> repository, ICrudRepository<HistoricalEvent> eventRepository, IMapper mapper) : base(repository, eventRepository)
        {
            _mapper = mapper;
        }

        public IEnumerable<TDto> GetAll() => base.GetAll().Select(_ => _mapper.Map<TDto>(_));

        public TDto GetById(Guid id) => _mapper.Map<TDto>(base.GetById(id));

        public TDto Create(TDto dto)
        {
            var createdEntity = base.Create(_mapper.Map<TEntity>(dto));
            return _mapper.Map<TDto>(createdEntity);
        }

        public TDto Update(Guid id, TDto dto)
        {
            var updatedEntity = base.Update(id, _mapper.Map<TEntity>(dto));
            return _mapper.Map<TDto>(updatedEntity);
        }

        public TDto Delete(Guid id)
        {
            var deletedEntity = base.Delete(id);
            return _mapper.Map<TDto>(deletedEntity);
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await base.GetAllAsync();
            return entities.Select(_ => _mapper.Map<TDto>(_));
        }

        public async Task<TDto> GetByIdAsync(Guid id)
        {
            var entity = await base.GetByIdAsync(id);
            return _mapper.Map<TDto>(entity);
        }

        public async Task<TDto> CreateAsync(TDto dto)
        {
            var createdEntity = await base.CreateAsync(_mapper.Map<TEntity>(dto));
            return _mapper.Map<TDto>(createdEntity);
        }

        public async Task<TDto> UpdateAsync(Guid id, TDto dto)
        {
            var updatedEntity = await base.UpdateAsync(id, _mapper.Map<TEntity>(dto));
            return _mapper.Map<TDto>(updatedEntity);
        }

        public async Task<TDto> DeleteAsync(Guid id)
        {
            var deletedEntity = await base.DeleteAsync(id);
            return _mapper.Map<TDto>(deletedEntity);
        }

        public TDto Restore(Guid id)
        {
            var restoredEntity = base.Restore(id);
            return _mapper.Map<TDto>(restoredEntity);
        }

        public async Task<TDto> RestoreAsync(Guid id)
        {
            var restoredEntity = await base.RestoreAsync(id);
            return _mapper.Map<TDto>(restoredEntity);
        }
    }
}
