using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;

namespace LSG.GenericCrud.Dto.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDto">The type of the dto.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="LSG.GenericCrud.Services.HistoricalCrudService{TEntity}" />
    /// <seealso cref="LSG.GenericCrud.Services.IHistoricalCrudService{TDto}" />
    public class HistoricalCrudService<TDto, TEntity> :
        ICrudService<TDto>,
        IHistoricalCrudService<TDto>
        where TDto : class, IEntity, new()
        where TEntity : class, IEntity, new()
    {
        private readonly IHistoricalCrudService<TEntity> _service;
        private readonly ICrudRepository _repository;
        private readonly IMapper _mapper;

        public HistoricalCrudService(IHistoricalCrudService<TEntity> service, ICrudRepository repository, IMapper mapper)
        {
            _service = service;
            _repository = repository;
            _mapper = mapper;
            AutoCommit = false;
        }

        public bool AutoCommit { get; set; }

        public virtual TDto Create(TDto dto) => CreateAsync(dto).GetAwaiter().GetResult();

        public virtual async Task<TDto> CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var createdEntity = await _service.CreateAsync(entity);
            return _mapper.Map<TDto>(createdEntity);
        }

        public virtual TDto Delete(Guid id) => DeleteAsync(id).GetAwaiter().GetResult();

        public virtual async Task<TDto> DeleteAsync(Guid id)
        {
            var deletedEntity = await _service.DeleteAsync(id);
            return _mapper.Map<TDto>(deletedEntity);
        }

        public Task<TDto> CopyAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TDto> GetAll() => GetAllAsync().GetAwaiter().GetResult();

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _service.GetAllAsync();
            return entities.Select(_ => _mapper.Map<TDto>(_));
        }

        public virtual async Task<TDto> GetByIdAsync(Guid id) => _mapper.Map<TDto>(await _service.GetByIdAsync(id));

        public virtual TDto GetById(Guid id) => GetByIdAsync(id).GetAwaiter().GetResult();

        public virtual IEnumerable<IEntity> GetHistory(Guid id) => GetHistoryAsync(id).GetAwaiter().GetResult();

        public virtual async Task<IEnumerable<IEntity>> GetHistoryAsync(Guid id) => await _service.GetHistoryAsync(id);
        public Task<TDto> CopyFromChangeset(Guid entityId, Guid changesetId)
        {
            throw new NotImplementedException();
        }

        public virtual TDto Restore(Guid id) => RestoreAsync(id).GetAwaiter().GetResult();

        public virtual async Task<TDto> RestoreAsync(Guid id)
        {
            var restoredEntity = await _service.RestoreAsync(id);
            return _mapper.Map<TDto>(restoredEntity);
        }

        public virtual TDto Update(Guid id, TDto dto) => UpdateAsync(id, dto).GetAwaiter().GetResult();

        public virtual async Task<TDto> UpdateAsync(Guid id, TDto dto)
        {
            var updatedEntity = await _service.UpdateAsync(id, _mapper.Map<TEntity>(dto));
            return _mapper.Map<TDto>(updatedEntity);
        }

        public Task MarkAllAsRead()
        {
            throw new NotImplementedException();
        }

        public Task MarkAllAsUnread()
        {
            throw new NotImplementedException();
        }

        public Task MarkOneAsRead(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task MarkOneAsUnread(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReadeableStatus<TDto>>> GetReadStatusAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ReadeableStatus<TDto>> GetReadStatusByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<object> Delta(Guid id, DeltaRequest request)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDto">The type of the dto.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="LSG.GenericCrud.Services.HistoricalCrudService{TEntity}" />
    /// <seealso cref="LSG.GenericCrud.Services.IHistoricalCrudService{TDto}" />
    public class HistoricalCrudService<TId, TDto, TEntity> :
        ICrudService<TId, TDto>,
        IHistoricalCrudService<TId, TDto>
        where TDto : class, IEntity<TId>, new()
        where TEntity : class, IEntity<TId>, new()
    {
        private readonly IHistoricalCrudService<TId, TEntity> _service;
        private readonly ICrudRepository _repository;
        private readonly IMapper _mapper;

        public HistoricalCrudService(IHistoricalCrudService<TId, TEntity> service, ICrudRepository repository, IMapper mapper)
        {
            _service = service;
            _repository = repository;
            _mapper = mapper;
            AutoCommit = false;
        }

        public bool AutoCommit { get; set; }

        public virtual TDto Create(TDto dto) => CreateAsync(dto).GetAwaiter().GetResult();

        public virtual async Task<TDto> CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var createdEntity = await _service.CreateAsync(entity);
            return _mapper.Map<TDto>(createdEntity);
        }

        public virtual TDto Delete(TId id) => DeleteAsync(id).GetAwaiter().GetResult();

        public virtual async Task<TDto> DeleteAsync(TId id)
        {
            var deletedEntity = await _service.DeleteAsync(id);
            return _mapper.Map<TDto>(deletedEntity);
        }

        public Task<TDto> CopyAsync(TId id)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TDto> GetAll() => GetAllAsync().GetAwaiter().GetResult();

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _service.GetAllAsync();
            return entities.Select(_ => _mapper.Map<TDto>(_));
        }

        public virtual async Task<TDto> GetByIdAsync(TId id) => _mapper.Map<TDto>(await _service.GetByIdAsync(id));

        public virtual TDto GetById(TId id) => GetByIdAsync(id).GetAwaiter().GetResult();

        public virtual IEnumerable<IEntity> GetHistory(TId id) => GetHistoryAsync(id).GetAwaiter().GetResult();

        public virtual async Task<IEnumerable<IEntity>> GetHistoryAsync(TId id) => await _service.GetHistoryAsync(id);
        public Task<TDto> CopyFromChangeset(TId entityId, Guid changesetId)
        {
            throw new NotImplementedException();
        }

        public virtual TDto Restore(TId id) => RestoreAsync(id).GetAwaiter().GetResult();

        public virtual async Task<TDto> RestoreAsync(TId id)
        {
            var restoredEntity = await _service.RestoreAsync(id);
            return _mapper.Map<TDto>(restoredEntity);
        }

        public virtual TDto Update(TId id, TDto dto) => UpdateAsync(id, dto).GetAwaiter().GetResult();

        public virtual async Task<TDto> UpdateAsync(TId id, TDto dto)
        {
            var updatedEntity = await _service.UpdateAsync(id, _mapper.Map<TEntity>(dto));
            return _mapper.Map<TDto>(updatedEntity);
        }

        public Task MarkAllAsRead()
        {
            throw new NotImplementedException();
        }

        public Task MarkAllAsUnread()
        {
            throw new NotImplementedException();
        }

        public Task MarkOneAsRead(TId id)
        {
            throw new NotImplementedException();
        }

        public Task MarkOneAsUnread(TId id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReadeableStatus<TDto>>> GetReadStatusAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ReadeableStatus<TDto>> GetReadStatusByIdAsync(TId id)
        {
            throw new NotImplementedException();
        }

        public Task<object> Delta(TId id, DeltaRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
