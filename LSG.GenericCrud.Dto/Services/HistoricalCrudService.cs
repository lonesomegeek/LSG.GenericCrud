using System;
using System.Collections.Generic;
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

        public IEnumerable<TDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public TDto GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public TDto Create(TDto entity)
        {
            throw new NotImplementedException();
        }

        public TDto Update(Guid id, TDto entity)
        {
            throw new NotImplementedException();
        }

        public TDto Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TDto> CreateAsync(TDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<TDto> UpdateAsync(Guid id, TDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<TDto> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public TDto Restore(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TDto> RestoreAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
