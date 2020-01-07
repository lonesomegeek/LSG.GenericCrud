using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Helpers;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

namespace LSG.GenericCrud.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="LSG.GenericCrud.Services.CrudService{T}" />
    /// <seealso cref="LSG.GenericCrud.Services.IHistoricalCrudService{T}" />
    // TODO: Mark async method as async in method name^
    public class HistoricalCrudService<T1, T2> :
        ICrudService<T1, T2>,
        IHistoricalCrudService<T1, T2> where T2 : class, IEntity<T1>, new()
    {
        private readonly ICrudService<T1, T2> _service;

        /// <summary>
        /// The repository
        /// </summary>
        private readonly ICrudRepository _repository;

        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IHistoricalCrudServiceOptions _options;
        private readonly IHistoricalCrudReadService<T1, T2> _historicalCrudReadService;

        public bool AutoCommit { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoricalCrudService{T}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="eventRepository">The event repository.</param>
        public HistoricalCrudService(
            ICrudService<T1, T2> service,
            ICrudRepository repository,
            IUserInfoRepository userInfoRepository,
            IHistoricalCrudReadService<T1, T2> historicalCrudReadService/*, IHistoricalCrudServiceOptions options*/)
        {
            _service = service;
            _repository = repository;
            _service.AutoCommit = false;
            _userInfoRepository = userInfoRepository;
            _historicalCrudReadService = historicalCrudReadService;

            // TODO: Remove mocking structure here, for v4.0, setup will be hardcoded, a new feature will cover a configurable option for this
            var optionsMock = new Mock<IHistoricalCrudServiceOptions>();
            optionsMock.Setup(_ => _.ShowMyNewStuff).Returns(true);
            _options = optionsMock.Object;

            AutoCommit = false;
        }

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<T2> CreateAsync(T2 entity)
        {
            var createdEntity = await _service.CreateAsync(entity);

            var historicalEvent = new HistoricalEvent
            {
                Action = HistoricalActions.Create.ToString(),
                Changeset = new HistoricalChangeset()
                {
                    ObjectDelta = new T2().DetailedCompare(entity)
                },
                EntityId = entity.Id.ToString(),
                EntityName = entity.GetType().FullName
            };

            await _repository.CreateAsync<Guid, HistoricalEvent>(historicalEvent);
            await _repository.SaveChangesAsync();

            return createdEntity;
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<T2> UpdateAsync(T1 id, T2 entity)
        {
            var originalEntity = await _service.GetByIdAsync(id);
            var historicalEvent = new HistoricalEvent
            {
                Action = HistoricalActions.Update.ToString(),
                Changeset = new HistoricalChangeset()
                {
                    ObjectData = JsonConvert.SerializeObject(originalEntity),
                    ObjectDelta = originalEntity.DetailedCompare(entity)
                },
                EntityId = originalEntity.Id.ToString(),
                EntityName = entity.GetType().FullName
            };
            var modifiedEntity = await _service.UpdateAsync(id, entity);

            await _repository.CreateAsync<Guid, HistoricalEvent>(historicalEvent);
            await _repository.SaveChangesAsync();

            return modifiedEntity;
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual async Task<T2> DeleteAsync(T1 id)
        {
            var entity = await _service.DeleteAsync(id);

            // store all object in historical event
            var historicalEvent = new HistoricalEvent
            {
                Action = HistoricalActions.Delete.ToString(),
                Changeset = new HistoricalChangeset()
                {
                    ObjectData = new T2().DetailedCompare(entity)
                },
                EntityId = entity.Id.ToString(),
                EntityName = entity.GetType().FullName
            };
            await _repository.CreateAsync<Guid, HistoricalEvent>(historicalEvent);
            await _repository.SaveChangesAsync();

            return entity;
        }

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<T2> CopyAsync(T1 id)
        {
            var createdEntity = await _service.CopyAsync(id);

            var historicalEvent = new HistoricalEvent
            {
                Action = HistoricalActions.Create.ToString(),
                Changeset = new HistoricalChangeset()
                {
                    ObjectDelta = new T2().DetailedCompare(createdEntity)
                },
                EntityId = createdEntity.Id.ToString(),
                EntityName = createdEntity.GetType().FullName
            };

            await _repository.CreateAsync<Guid, HistoricalEvent>(historicalEvent);
            await _repository.SaveChangesAsync();

            return createdEntity;
        }

        /// <summary>
        /// Restores the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="LSG.GenericCrud.Exceptions.EntityNotFoundException"></exception>
        public virtual async Task<T2> RestoreAsync(T1 id)
        {
            var historicalEvent = _repository
                .GetAllAsync<Guid, HistoricalEvent>()
                .Result
                .SingleOrDefault(_ =>
                    _.EntityId == id.ToString() &&
                    _.Action == HistoricalActions.Delete.ToString());
            if (historicalEvent == null) throw new EventNotFoundException();
            var changeset = _repository
                .GetAllAsync<Guid, HistoricalChangeset>()
                .Result
                .SingleOrDefault(_ => _.EventId == historicalEvent.Id);
            if (changeset == null) throw new ChangesetNotFoundException();

            var json = changeset.ObjectData;
            var obj = JsonConvert.DeserializeObject<T2>(json);
            var createdObject = await CreateAsync(obj);

            return createdObject;
        }

        public virtual async Task<T2> RestoreFromChangeset(T1 entityId, Guid changesetId)
        {
            var entity = await _repository.GetByIdAsync<T1, T2>(entityId);
            if (entity == null) throw new EntityNotFoundException();
            entity = entity.CopyObject(); // TODO: Validate if it can be cutified, object need to be datached from its context
            var changeset = await _repository.GetByIdAsync<Guid, HistoricalChangeset>(changesetId);
            if (changeset == null) throw new ChangesetNotFoundException();

            var actualDelta = JsonConvert.DeserializeObject<T2>(changeset.ObjectDelta);
            var restoredEntity = entity.ApplyChangeset(actualDelta);
            entity.ApplyChangeset(restoredEntity);

            return await UpdateAsync(entityId, entity);
        }

        /// <summary>
        /// Gets the history asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="LSG.GenericCrud.Exceptions.EntityNotFoundException"></exception>
        public virtual async Task<IEnumerable<IEntity>> GetHistoryAsync(T1 id)
        {
            var events = await _repository.GetAllAsync<Guid, HistoricalEvent>();
            var filteredEvents = events
                .Where(_ => _.EntityId == id.ToString())
                .ToList();
            if (!filteredEvents.Any()) throw new EntityNotFoundException();
            return filteredEvents;
        }

        public virtual async Task<T2> CopyFromChangeset(T1 entityId, Guid changesetId)
        {
            var entity = await _repository.GetByIdAsync<T1, T2>(entityId);
            if (entity == null) throw new EntityNotFoundException();
            var changeset = await _repository.GetByIdAsync<Guid, HistoricalChangeset>(changesetId);
            if (changeset == null) throw new ChangesetNotFoundException();

            var actualObject = changeset.ObjectData == null ? new T2() : JsonConvert.DeserializeObject<T2>(changeset.ObjectData);
            var actualDelta = JsonConvert.DeserializeObject<T2>(changeset.ObjectDelta);
            var actualEntity = actualObject.ApplyChangeset(actualDelta);
            var copiedEntity = actualEntity.CopyObject();

            return await CreateAsync(copiedEntity); ;
        }

        public virtual async Task MarkAllAsRead()
        {
            // TODO: Check if the userinfo repository is available, if not, throw exception
            var entities = await _repository.GetAllAsync<T1, T2>();
            foreach (var entity in entities)
            {
                var historicalEvent = new HistoricalEvent
                {
                    Action = HistoricalActions.Read.ToString(),
                    EntityId = entity.Id.ToString(),
                    EntityName = entity.GetType().FullName
                };

                await _repository.CreateAsync<Guid, HistoricalEvent>(historicalEvent);
            }

            await _repository.SaveChangesAsync();
        }

        public virtual async Task MarkAllAsUnread()
        {
            // TODO: Check if the userinfo repository is available, if not, throw exception

            var events = await _repository.GetAllAsync<Guid, HistoricalEvent>();
            events
                .Where(_ =>
                    _.Action == HistoricalActions.Read.ToString() &&
                    _.EntityName == typeof(T2).FullName &&
                    _.CreatedBy == _userInfoRepository.GetUserInfo())
                .ToList()
                .ForEach(async _ => await _repository.DeleteAsync<Guid, HistoricalEvent>(_.Id));
            await _repository.SaveChangesAsync();
        }

        public virtual async Task MarkOneAsRead(T1 id)
        {
            var entity = await _repository.GetByIdAsync<T1, T2>(id);
            var historicalEvent = new HistoricalEvent
            {
                Action = HistoricalActions.Read.ToString(),
                EntityId = entity.Id.ToString(),
                EntityName = entity.GetType().FullName
            };

            await _repository.CreateAsync<Guid, HistoricalEvent>(historicalEvent);
            await _repository.SaveChangesAsync();
        }

        public virtual async Task MarkOneAsUnread(T1 id)
        {
            var events = await _repository.GetAllAsync<Guid, HistoricalEvent>();
            events
                .Where(_ => _.EntityName == typeof(T2).FullName &&
                            _.EntityId == id.ToString() &&
                            _.CreatedBy == _userInfoRepository.GetUserInfo())
                .ToList()
                .ForEach(async _ => await _repository.DeleteAsync<Guid, HistoricalEvent>(_.Id));
            await _repository.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<ReadeableStatus<T2>>> GetReadStatusAsync()
        {
            var readEvents = await _repository.GetAllAsync<Guid, HistoricalEvent>();
            var entityName = typeof(T2).FullName;
            var entities = Task.Run(() => _repository.GetAllAsync<T1, T2>()).Result.ToList();
            return entities
                .Select(entity =>
                {
                    var historicalEvent = readEvents
                        .Where(e => e.EntityId == entity.Id.ToString() &&
                                    e.EntityName == entityName &&
                                    e.CreatedBy == _userInfoRepository.GetUserInfo())
                        .OrderBy(_ => _.CreatedDate)
                        .LastOrDefault();

                    return new ReadeableStatus<T2>()
                    {
                        Data = entity,
                        Metadata = new ReadeableStatusMetadata()
                        {
                            LastViewed = historicalEvent?.CreatedDate,
                            NewStuffAvailable = IsNewStuffAvailable(entity, historicalEvent)
                        }
                    };
                });
        }

        public virtual async Task<ReadeableStatus<T2>> GetReadStatusByIdAsync(T1 id)
        {

            var entityName = typeof(T2).FullName;
            var readEvents = _repository
                .GetAllAsync<Guid, HistoricalEvent>()
                .Result
                .Where(_ => _.EntityId == id.ToString() &&
                            _.EntityName == entityName &&
                            _.CreatedBy == _userInfoRepository.GetUserInfo())
                .OrderBy(_ => _.CreatedDate);

            var entity = await _repository.GetByIdAsync<T1, T2>(id);

            return new ReadeableStatus<T2>()
            {
                Data = entity,
                Metadata = new ReadeableStatusMetadata()
                {
                    LastViewed = readEvents.LastOrDefault()?.CreatedDate,
                    NewStuffAvailable = IsNewStuffAvailable(entity, readEvents.LastOrDefault())
                }
            }; ;

        }

        private bool IsNewStuffAvailable(T2 entity, HistoricalEvent historicalEvent)
        {
            if (!(entity is ICreatedInfo && entity is IModifiedInfo)) throw new NotSupportedException("Entity must implement ICreatedInfo and IModifiedInfo");
            var modifiedInfo = entity as IModifiedInfo;
            var createdInfo = entity as ICreatedInfo;

            var createdByMe = modifiedInfo.ModifiedBy == null && createdInfo.CreatedBy == _userInfoRepository.GetUserInfo();
            var modifiedByMe = modifiedInfo.ModifiedBy != null && modifiedInfo.ModifiedBy == _userInfoRepository.GetUserInfo();
            var createdOrModifiedByMe = createdByMe || modifiedByMe;

            var createdBySomeone = modifiedInfo.ModifiedBy == null && createdInfo.CreatedBy != _userInfoRepository.GetUserInfo();
            var modifiedBySomeone = modifiedInfo.ModifiedBy != null && modifiedInfo.ModifiedBy != _userInfoRepository.GetUserInfo();
            var createdOrModifiedBySomeone = createdBySomeone || modifiedBySomeone;

            var viewedLately = modifiedInfo.ModifiedDate == null && historicalEvent?.CreatedDate > createdInfo.CreatedDate || modifiedInfo.ModifiedDate != null && historicalEvent?.CreatedDate > modifiedInfo.ModifiedDate;

            if (!viewedLately)
            {
                if (_options.ShowMyNewStuff && createdOrModifiedByMe) return true;
                else if (_options.ShowMyNewStuff && createdOrModifiedBySomeone) return true;
                else if (!_options.ShowMyNewStuff && createdOrModifiedBySomeone) return true;
                else return false;
            }
            else
                return false;
        }

        public virtual async Task<object> Delta(T1 id, DeltaRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            if (request.From == null) request.From = _historicalCrudReadService.GetLastTimeViewed<T2>(id);
            if (request.To == null) request.To = DateTime.MaxValue;

            if (request.Mode == DeltaRequestModes.Snapshot) return await GetDeltaSnapshot(id, request.From.Value, request.To.Value);
            return await GetDeltaDifferential(id, request.From.Value, request.To.Value);

        }

        public async Task<SnapshotChangeset> GetDeltaSnapshot(T1 id, DateTime fromTimestamp, DateTime toTimestamp)
        {
            var historicalEvents = await _repository.GetAllAsync<Guid, HistoricalEvent>();
            var historicalChangesets = await _repository.GetAllAsync<Guid, HistoricalChangeset>();

            var events =
                from e in historicalEvents
                join c in historicalChangesets on e.Id equals c.EventId
                where e.EntityId == id.ToString() && c.CreatedDate >= fromTimestamp && c.CreatedDate <= toTimestamp
                select e;

            if (!events.Any()) throw new NoHistoryException();

            var entity = await _repository.GetByIdAsync<T1, T2>(id);

            return ExtractSnapshotChanges(events, entity);
        }
        public async Task<DifferentialChangeset> GetDeltaDifferential(T1 id, DateTime fromTimestamp, DateTime toTimestamp)
        {
            // snapshot from creation date
            var events = _repository
                .GetAll<Guid, HistoricalEvent>()
                .Where(_ => _.EntityId == id.ToString() && _.CreatedDate >= fromTimestamp && _.CreatedDate <= toTimestamp && _.Action != HistoricalActions.Read.ToString())
                .ToList()
                .OrderBy(_ => _.CreatedDate);

            if (events == null || !events.Any()) throw new NoHistoryException();
            //.Skip(1);

            return await ExtractDifferentialChangeset(id, events);
        }

        public async Task<DifferentialChangeset> ExtractDifferentialChangeset(T1 id, IOrderedEnumerable<HistoricalEvent> events)
        {
            var changesets = await _repository.GetAllAsync<Guid, HistoricalChangeset>(); // TODO: keep it there, include changesets in context

            var differentialChangeset = new DifferentialChangeset();
            differentialChangeset.EntityId = id.ToString();
            differentialChangeset.EntityTypeName = events.First().EntityName;
            differentialChangeset.Changesets = events.AggregateCombine(ExtractOneDifferentialChangeset<T2>);
            return differentialChangeset;
        }

        public Changeset ExtractOneDifferentialChangeset<T2>(HistoricalEvent currentEvent, HistoricalEvent nextEvent) where T2 : class, IEntity<T1>, new()
        {
            var currentObject = JsonConvert.DeserializeObject<T2>(currentEvent.Changeset.ObjectDelta);
            var nextObject =
                nextEvent.Action == HistoricalActions.Delete.ToString()
                    ? JsonConvert.DeserializeObject<T2>(nextEvent.Changeset?.ObjectData)
                    : JsonConvert.DeserializeObject<T2>(nextEvent.Changeset?.ObjectDelta);

            var changeset = new Changeset();
            changeset.EventDate = nextEvent.CreatedDate.Value;
            changeset.UserId = nextEvent.CreatedBy;
            changeset.ChangesetId = nextEvent.Changeset.Id;
            changeset.EventName = nextEvent.Action;
            changeset.Changes = 
                currentEvent.Action != HistoricalActions.Delete.ToString() ? 
                    _historicalCrudReadService.ExtractChanges(currentObject, nextObject) : 
                    null;

            return changeset;
        }

        public SnapshotChangeset ExtractSnapshotChanges(IEnumerable<HistoricalEvent> events, T2 actual)
        {
            var sourceEvent = events.FirstOrDefault();

            var sourceObject = sourceEvent.Changeset == null ?
                JsonConvert.DeserializeObject<T2>(sourceEvent.Changeset.ObjectData) :
                JsonConvert.DeserializeObject<T2>(sourceEvent.Changeset.ObjectDelta);

            var snapshotChangeset = new SnapshotChangeset();
            snapshotChangeset.EntityTypeName = sourceEvent.EntityName;
            snapshotChangeset.EntityId = sourceEvent.EntityId;
            snapshotChangeset.LastViewed = _historicalCrudReadService.GetLastTimeViewed<T2>(actual.Id).Value;
            snapshotChangeset.LastModifiedBy = events.Last().CreatedBy;
            snapshotChangeset.LastModifiedEvent = events.Last().Action;
            snapshotChangeset.LastModifiedDate = events.Last().CreatedDate.Value;
            // TODO: Bug here if entity is deleted or not found
            snapshotChangeset.Changes = _historicalCrudReadService.ExtractChanges(sourceObject, actual);
            return snapshotChangeset;
        }

        public virtual async Task<IEnumerable<T2>> GetAllAsync() => await _service.GetAllAsync();

        public virtual async Task<T2> GetByIdAsync(T1 id) => await _service.GetByIdAsync(id);
    }
}
