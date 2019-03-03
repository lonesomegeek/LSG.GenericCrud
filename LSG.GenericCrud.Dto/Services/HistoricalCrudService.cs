using AutoMapper;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Helpers;
using Newtonsoft.Json;

namespace LSG.GenericCrud.Dto.Services
{
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <typeparam name="TDto">The type of the dto.</typeparam>
    ///// <typeparam name="TEntity">The type of the entity.</typeparam>
    ///// <seealso cref="LSG.GenericCrud.Services.HistoricalCrudService{TEntity}" />
    ///// <seealso cref="LSG.GenericCrud.Services.IHistoricalCrudService{TDto}" />
    //public class HistoricalCrudService<TDto, TEntity> :
    //    ICrudService<TDto>,
    //    IHistoricalCrudService<TDto>
    //    where TDto : class, IEntity, new()
    //    where TEntity : class, IEntity, new()
    //{
    //    private readonly IHistoricalCrudService<TEntity> _service;
    //    private readonly ICrudRepository _repository;
    //    private readonly IMapper _mapper;

    //    public HistoricalCrudService(IHistoricalCrudService<TEntity> service, ICrudRepository repository, IMapper mapper)
    //    {
    //        _service = service;
    //        _repository = repository;
    //        _mapper = mapper;
    //        AutoCommit = false;
    //    }

    //    public bool AutoCommit { get; set; }

    //    public virtual TDto Create(TDto dto) => CreateAsync(dto).GetAwaiter().GetResult();

    //    public virtual async Task<TDto> CreateAsync(TDto dto)
    //    {
    //        var entity = _mapper.Map<TEntity>(dto);
    //        var createdEntity = await _service.CreateAsync(entity);
    //        return _mapper.Map<TDto>(createdEntity);
    //    }

    //    public virtual TDto Delete(Guid id) => DeleteAsync(id).GetAwaiter().GetResult();

    //    public virtual async Task<TDto> DeleteAsync(Guid id)
    //    {
    //        var deletedEntity = await _service.DeleteAsync(id);
    //        return _mapper.Map<TDto>(deletedEntity);
    //    }

    //    public Task<TDto> CopyAsync(Guid id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public virtual IEnumerable<TDto> GetAll() => GetAllAsync().GetAwaiter().GetResult();

    //    public virtual async Task<IEnumerable<TDto>> GetAllAsync()
    //    {
    //        var entities = await _service.GetAllAsync();
    //        return entities.Select(_ => _mapper.Map<TDto>(_));
    //    }

    //    public virtual async Task<TDto> GetByIdAsync(Guid id) => _mapper.Map<TDto>(await _service.GetByIdAsync(id));

    //    public virtual TDto GetById(Guid id) => GetByIdAsync(id).GetAwaiter().GetResult();

    //    public virtual IEnumerable<IEntity> GetHistory(Guid id) => GetHistoryAsync(id).GetAwaiter().GetResult();

    //    public virtual async Task<TDto> RestoreFromChangeset(Guid entityId, Guid changesetId) => throw new NotImplementedException();

    //    public virtual async Task<IEnumerable<IEntity>> GetHistoryAsync(Guid id) => await _service.GetHistoryAsync(id);
    //    public Task<TDto> CopyFromChangeset(Guid entityId, Guid changesetId)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public virtual TDto Restore(Guid id) => RestoreAsync(id).GetAwaiter().GetResult();

    //    public virtual async Task<TDto> RestoreAsync(Guid id)
    //    {
    //        var restoredEntity = await _service.RestoreAsync(id);
    //        return _mapper.Map<TDto>(restoredEntity);
    //    }

    //    public virtual TDto Update(Guid id, TDto dto) => UpdateAsync(id, dto).GetAwaiter().GetResult();

    //    public virtual async Task<TDto> UpdateAsync(Guid id, TDto dto)
    //    {
    //        var updatedEntity = await _service.UpdateAsync(id, _mapper.Map<TEntity>(dto));
    //        return _mapper.Map<TDto>(updatedEntity);
    //    }

    //    public Task MarkAllAsRead()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task MarkAllAsUnread()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task MarkOneAsRead(Guid id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task MarkOneAsUnread(Guid id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IEnumerable<ReadeableStatus<TDto>>> GetReadStatusAsync()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<ReadeableStatus<TDto>> GetReadStatusByIdAsync(Guid id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<object> Delta(Guid id, DeltaRequest request)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

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
        private readonly IUserInfoRepository _userInfoRepository;

        public HistoricalCrudService(
            IHistoricalCrudService<TId, TEntity> service,
            ICrudRepository repository,
            IUserInfoRepository userInfoRepository,
            IMapper mapper)
        {
            _service = service;
            _repository = repository;
            _userInfoRepository = userInfoRepository;
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

        public virtual async Task<TDto> CopyAsync(TId id) => _mapper.Map<TDto>(await _service.CopyAsync(id));

        public virtual IEnumerable<TDto> GetAll() => GetAllAsync().GetAwaiter().GetResult();

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _service.GetAllAsync();
            return entities.Select(_ => _mapper.Map<TDto>(_));
        }

        public virtual async Task<TDto> GetByIdAsync(TId id) => _mapper.Map<TDto>(await _service.GetByIdAsync(id));

        public virtual TDto GetById(TId id) => GetByIdAsync(id).GetAwaiter().GetResult();

        public virtual IEnumerable<IEntity> GetHistory(TId id) => GetHistoryAsync(id).GetAwaiter().GetResult();

        public virtual async Task<TDto> RestoreFromChangeset(TId entityId, Guid changesetId) => _mapper.Map<TDto>(await _service.RestoreFromChangeset(entityId, changesetId));

        public virtual async Task<IEnumerable<IEntity>> GetHistoryAsync(TId id) => await _service.GetHistoryAsync(id);
        public virtual async Task<TDto> CopyFromChangeset(TId entityId, Guid changesetId) => _mapper.Map<TDto>(await _service.CopyFromChangeset(entityId, changesetId));
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

        public virtual async Task MarkAllAsRead() => await _service.MarkAllAsRead();

        public virtual async Task MarkAllAsUnread() => await _service.MarkAllAsUnread();

        public virtual async Task MarkOneAsRead(TId id) => await _service.MarkOneAsRead(id);

        public virtual async Task MarkOneAsUnread(TId id) => await _service.MarkOneAsUnread(id);

        public virtual async Task<IEnumerable<ReadeableStatus<TDto>>> GetReadStatusAsync()
        {
            var statuses = await _service.GetReadStatusAsync();
            return statuses.Select(_ => new ReadeableStatus<TDto>()
            {
                Data = _mapper.Map<TDto>(_.Data),
                Metadata = _.Metadata
            });
        }

        public virtual async Task<ReadeableStatus<TDto>> GetReadStatusByIdAsync(TId id)
        {
            var status = await _service.GetReadStatusByIdAsync(id);
            return new ReadeableStatus<TDto>()
            {
                Data = _mapper.Map<TDto>(status.Data),
                Metadata = status.Metadata
            };
        }

        // TODO: Adapt for dto object, should not present entity values
        public virtual async Task<object> Delta(TId id, DeltaRequest request)
        {
            if (request.From == null) request.From = GetLastTimeViewed<TEntity>(id);
            if (request.To == null) request.To = DateTime.MaxValue;
            if (request.Mode == DeltaRequestModes.Snapshot) return await GetDeltaSnapshot(id, request.From.Value, request.To.Value);
            else if (request.Mode == DeltaRequestModes.Differential) return await GetDeltaDifferential(id, request.From.Value, request.To.Value);
            throw new NotImplementedException();
            // TODO: Convert TEntity to TDto
        }

        public DateTime? GetLastTimeViewed<TEntity>(TId id)
        {
            var lastView = _repository
                .GetAll<HistoricalEvent>()
                .SingleOrDefault(_ =>
                    _.EntityId == id.ToString() &&
                    _.EntityName == typeof(TEntity).FullName &&
                    _.Action == HistoricalActions.Read.ToString() &&
                    _.CreatedBy == _userInfoRepository.GetUserInfo());
            return lastView?.CreatedDate ?? DateTime.MinValue;
        }
        public async Task<SnapshotChangeset> GetDeltaSnapshot(TId id, DateTime fromTimestamp, DateTime toTimestamp)
        {
            var events =
                from e in _repository.GetAll<HistoricalEvent>()
                join c in _repository.GetAllAsync<Guid, HistoricalChangeset>().Result on e.Id equals c.EventId
                where e.EntityId == id.ToString() && c.CreatedDate >= fromTimestamp && c.CreatedDate <= toTimestamp
                select e;

            if (!events.Any()) throw new NoHistoryException();

            var entity = await _repository.GetByIdAsync<TId, TEntity>(id);

            return ExtractSnapshotChanges(events, entity);
        }
        public async Task<DifferentialChangeset> GetDeltaDifferential(TId id, DateTime fromTimestamp, DateTime toTimestamp)
        {
            // snapshot from creation date
            var events = _repository
                .GetAll<HistoricalEvent>()
                .Where(_ => _.EntityId == id.ToString() && _.CreatedDate >= fromTimestamp && _.CreatedDate <= toTimestamp && _.Action != HistoricalActions.Read.ToString())
                .OrderBy(_ => _.CreatedDate);

            if (events.Count() == 0) throw new NoHistoryException();
            //.Skip(1);

            var differentialChangeset = await ExtractDifferentialChangeset(id, events);

            return differentialChangeset;
        }
        private SnapshotChangeset ExtractSnapshotChanges(IEnumerable<HistoricalEvent> events, TEntity actual)
        {
            var actualDto = _mapper.Map<TDto>(actual);
            var sourceEvent = events.FirstOrDefault();

            var sourceObject = sourceEvent.Changeset == null ?
                JsonConvert.DeserializeObject<TEntity>(sourceEvent.Changeset.ObjectData) :
                JsonConvert.DeserializeObject<TEntity>(sourceEvent.Changeset.ObjectDelta);
            var sourceDto = _mapper.Map<TDto>(sourceObject);

            var snapshotChangeset = new SnapshotChangeset();
            snapshotChangeset.EntityTypeName = sourceEvent.EntityName;
            snapshotChangeset.EntityId = sourceEvent.EntityId;
            snapshotChangeset.LastViewed = DateTime.Now; // TODO: Get Last Viewed Info from read status (if available)
            snapshotChangeset.LastModifiedBy = events.Last().CreatedBy;
            snapshotChangeset.LastModifiedEvent = events.Last().Action;
            snapshotChangeset.LastModifiedDate = events.Last().CreatedDate.Value;
            snapshotChangeset.Changes = ExtractChanges(sourceDto, actualDto);

            return snapshotChangeset;
        }

        private async Task<DifferentialChangeset> ExtractDifferentialChangeset(TId id, IOrderedEnumerable<HistoricalEvent> events)
        {
            var changesets = await _repository.GetAllAsync<Guid, HistoricalChangeset>();
            var changeset = changesets.FirstOrDefault();
            var sourceEvent = events.First();
            var sourceObject = sourceEvent.Changeset.ObjectData == null ? JsonConvert.DeserializeObject<TEntity>(sourceEvent.Changeset.ObjectDelta) : JsonConvert.DeserializeObject<TEntity>(sourceEvent.Changeset.ObjectData);

            var differentialChangeset = new DifferentialChangeset();
            differentialChangeset.EntityId = id.ToString();
            differentialChangeset.EntityTypeName = sourceEvent.EntityName;
            differentialChangeset.Changesets = ExtractDifferentialChanges(id, events, sourceEvent, sourceObject);
            return differentialChangeset;
        }

        private List<Changeset> ExtractDifferentialChanges(TId id, IOrderedEnumerable<HistoricalEvent> events, HistoricalEvent sourceEvent, TEntity sourceObject)
        {

            var differentialChangeset = new List<Changeset>();

            var currentEvent = sourceEvent;
            var currentObject = sourceObject;
            var currentDto = _mapper.Map<TDto>(currentObject);
            for (int i = 1; i < events.Count(); i++)
            {
                var nextEvent = events.ToArray()[i];
                var nextEventObject = JsonConvert.DeserializeObject<TEntity>(currentEvent.Changeset.ObjectDelta);
                var nextEventDto = _mapper.Map<TDto>(nextEventObject);
                var changeset = new Changeset();
                changeset.EventDate = currentEvent.CreatedDate.Value;
                changeset.UserId = currentEvent.CreatedBy;
                changeset.ChangesetId = currentEvent.Changeset.Id;
                changeset.EventName = currentEvent.Action;
                changeset.Changes = ExtractChanges(currentDto, nextEventDto);
                differentialChangeset.Add(changeset);

                currentObject = nextEventObject;
                currentDto = _mapper.Map<TDto>(currentObject);
                currentEvent = nextEvent;
            }
            // add last event to current object
            var lastChangeset = new Changeset();
            var lastEvent = events.Last();

            lastChangeset.EventDate = lastEvent.CreatedDate.Value;
            lastChangeset.UserId = lastEvent.CreatedBy;
            lastChangeset.ChangesetId = lastEvent.Changeset.Id;
            lastChangeset.Changes = lastEvent.Action != HistoricalActions.Delete.ToString() ? ExtractChanges(
                _mapper.Map<TDto>(currentObject),
                _mapper.Map<TDto>(_repository.GetById<TId, TEntity>(id))) : null;
            lastChangeset.EventName = lastEvent.Action;

            differentialChangeset.Add(lastChangeset);

            return differentialChangeset;
        }

        // TODO: Place that in interface for overrideable definition
        public List<Change> ExtractChanges<T>(T source, T destination)
        {
            if (destination == null) throw new ArgumentNullException(nameof(destination));
            var changes = new List<Change>();

            destination
                .GetType()
                .GetProperties()
                .Where(_ => _.DeclaringType == destination.GetType() && !Attribute.IsDefined(_, typeof(IgnoreInChangesetAttribute)))
                .ToList()
                .ForEach(_ => changes.Add(new Change()
                {
                    FieldName = _.Name,
                    FromValue = source.GetType().GetProperty(_.Name)?.GetValue(source),
                    ToValue = destination.GetType().GetProperty(_.Name)?.GetValue(destination)
                }));

            return changes;
        }

    }
}
