using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Helpers;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.AspNetCore.Hosting.Internal;
using Moq;
using Newtonsoft.Json;
using Remotion.Linq.Clauses;

namespace LSG.GenericCrud.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="LSG.GenericCrud.Services.CrudService{T}" />
    /// <seealso cref="LSG.GenericCrud.Services.IHistoricalCrudService{T}" />
    public class HistoricalCrudService<T> :
        IHistoricalCrudService<Guid, T> where T : class, IEntity, new()
    {
        private readonly IHistoricalCrudService<Guid, T> _service;

        public HistoricalCrudService(IHistoricalCrudService<Guid, T> service)
        {
            _service = service;
            AutoCommit = false;
        }

        public bool AutoCommit { get; set; }
        public virtual IEnumerable<T> GetAll() => _service.GetAll();
        public virtual T GetById(Guid id) => _service.GetById(id);
        public virtual T Create(T entity) => _service.Create(entity);
        public virtual T Update(Guid id, T entity) => _service.Update(id, entity);
        public virtual T Delete(Guid id) => _service.Delete(id);
        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _service.GetAllAsync();
        public virtual async Task<T> GetByIdAsync(Guid id) => await _service.GetByIdAsync(id);
        public virtual async Task<T> CreateAsync(T entity) => await _service.CreateAsync(entity);
        public virtual async Task<T> UpdateAsync(Guid id, T entity) => await _service.UpdateAsync(id, entity);
        public virtual async Task<T> DeleteAsync(Guid id) => await _service.DeleteAsync(id);
        public virtual async Task<T> CopyAsync(Guid id) => await _service.CopyAsync(id);
        public virtual T Restore(Guid id) => _service.Restore(id);        
        public virtual IEnumerable<IEntity> GetHistory(Guid id) => _service.GetHistory(id);
        public virtual async Task<T> RestoreAsync(Guid id) => await _service.RestoreAsync(id);
        public virtual async Task<T> RestoreFromChangeset(Guid entityId, Guid changesetId) => await _service.RestoreFromChangeset(entityId, changesetId);
        public virtual async Task<IEnumerable<IEntity>> GetHistoryAsync(Guid id) => await _service.GetHistoryAsync(id);
        public virtual async Task<T> CopyFromChangeset(Guid entityId, Guid changesetId) => await _service.CopyFromChangeset(entityId, changesetId);
        public virtual async Task MarkAllAsRead() => await _service.MarkAllAsRead();
        public virtual async Task MarkAllAsUnread() => await _service.MarkAllAsUnread();
        public virtual async Task MarkOneAsRead(Guid id) => await MarkOneAsRead(id);
        public virtual async Task MarkOneAsUnread(Guid id) => await MarkOneAsUnread(id);
        public virtual async Task<IEnumerable<ReadeableStatus<T>>> GetReadStatusAsync() => await _service.GetReadStatusAsync();
        public virtual async Task<ReadeableStatus<T>> GetReadStatusByIdAsync(Guid id) => await _service.GetReadStatusByIdAsync(id);
        public virtual async Task<object> Delta(Guid id, DeltaRequest request) => await _service.Delta(id, request);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="LSG.GenericCrud.Services.CrudService{T}" />
    /// <seealso cref="LSG.GenericCrud.Services.IHistoricalCrudService{T}" />
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

        public bool AutoCommit { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoricalCrudService{T}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="eventRepository">The event repository.</param>
        public HistoricalCrudService(
            ICrudService<T1, T2> service,
            ICrudRepository repository,
            IUserInfoRepository userInfoRepository/*, IHistoricalCrudServiceOptions options*/)
        {
            _service = service;
            _repository = repository;
            _service.AutoCommit = false;
            _userInfoRepository = userInfoRepository;
            var optionsMock = new Mock<IHistoricalCrudServiceOptions>();
            optionsMock.Setup(_ => _.ShowMyNewStuff).Returns(true);
            _options = optionsMock.Object;
            AutoCommit = false;
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual T2 Create(T2 entity) => CreateAsync(entity).GetAwaiter().GetResult();

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
                EntityId = entity.Id.ToString(), // TODO: I do not like the string value compare here
                EntityName = entity.GetType().FullName
            };

            await _repository.CreateAsync<Guid, HistoricalEvent>(historicalEvent);
            await _repository.SaveChangesAsync();
            // TODO: Do I need to call the other repo for both repositories, or do I need a UoW (bugfix created)
            return createdEntity;
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual T2 Update(T1 id, T2 entity) => UpdateAsync(id, entity).GetAwaiter().GetResult();

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
                EntityId = originalEntity.Id.ToString(), // TODO: I do not like the string value compare here
                EntityName = entity.GetType().FullName
            };
            var modifiedEntity = await _service.UpdateAsync(id, entity);

            await _repository.CreateAsync<Guid, HistoricalEvent>(historicalEvent);
            await _repository.SaveChangesAsync();

            return modifiedEntity;
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual T2 Delete(T1 id) => DeleteAsync(id).GetAwaiter().GetResult();

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
                EntityId = entity.Id.ToString(), // TODO: I do not like the string value compare here
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
                EntityId = createdEntity.Id.ToString(), // TODO: I do not like the string value compare here
                EntityName = createdEntity.GetType().FullName
            };

            await _repository.CreateAsync<Guid, HistoricalEvent>(historicalEvent);
            await _repository.SaveChangesAsync();
            // TODO: Do I need to call the other repo for both repositories, or do I need a UoW (bugfix created)
            return createdEntity;
        }

        /// <summary>
        /// Restores the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="LSG.GenericCrud.Exceptions.EntityNotFoundException"></exception>
        public virtual T2 Restore(T1 id) => RestoreAsync(id).GetAwaiter().GetResult();

        /// <summary>
        /// Restores the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="LSG.GenericCrud.Exceptions.EntityNotFoundException"></exception>
        public virtual async Task<T2> RestoreAsync(T1 id)
        {
            var entity = _repository
                .GetAllAsync<Guid, HistoricalEvent>()
                .Result
                .SingleOrDefault(_ =>
                    _.EntityId == id.ToString() && // TODO: I do not like the string value compare here
                    _.Action == HistoricalActions.Delete.ToString());
            if (entity == null) throw new EntityNotFoundException();
            var json = entity.Changeset.ObjectData;
            var obj = JsonConvert.DeserializeObject<T2>(json);
            var createdObject = await CreateAsync(obj);

            return createdObject;
        }

        public virtual async Task<T2> RestoreFromChangeset(T1 entityId, Guid changesetId)
        {
            var entity = await _repository.GetByIdAsync<T1, T2>(entityId);
            entity = entity.CopyObject(); // TODO: Validate if it can be cutified, object need to be datached from its context
            if (entity == null) throw new EntityNotFoundException();
            var changeset = await _repository.GetByIdAsync<Guid, HistoricalChangeset>(changesetId);
            if (changeset == null) throw new ChangesetNotFoundException();

            //var actualObject = changeset.ObjectData == null ? new T2() : JsonConvert.DeserializeObject<T2>(changeset.ObjectData);
            var actualDelta = JsonConvert.DeserializeObject<T2>(changeset.ObjectDelta);
            var restoredEntity = entity.ApplyChangeset(actualDelta);
            entity.ApplyChangeset(restoredEntity);
            
            return await UpdateAsync(entityId, entity);
        }

        /// <summary>
        /// Gets the history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="LSG.GenericCrud.Exceptions.EntityNotFoundException"></exception>
        public virtual IEnumerable<IEntity> GetHistory(T1 id) => GetHistoryAsync(id).GetAwaiter().GetResult();

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
                .Where(_ => _.EntityId == id.ToString()) // TODO: I do not like the string value compare here
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
                    EntityId = entity.Id.ToString(), // TODO: I do not like the string value compare here
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
                .ForEach(async _ => await _repository.DeleteAsync<HistoricalEvent>(_.Id));
            await _repository.SaveChangesAsync();
        }

        public virtual async Task MarkOneAsRead(T1 id)
        {
            var entity = await _repository.GetByIdAsync<T1, T2>(id);
            var historicalEvent = new HistoricalEvent
            {
                Action = HistoricalActions.Read.ToString(),
                EntityId = entity.Id.ToString(), // TODO: I do not like the string value compare here
                EntityName = entity.GetType().FullName
            };

            await _repository.CreateAsync<Guid, HistoricalEvent>(historicalEvent);
            await _repository.SaveChangesAsync();
        }

        public virtual async Task MarkOneAsUnread(T1 id)
        {
            var events = await _repository.GetAllAsync<HistoricalEvent>();
            events
                .Where(_ => _.EntityName == typeof(T2).FullName &&
                            _.EntityId == id.ToString() &&
                            _.CreatedBy == _userInfoRepository.GetUserInfo())
                .ToList()
                .ForEach(async _ => await _repository.DeleteAsync<HistoricalEvent>(_.Id));
            await _repository.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<ReadeableStatus<T2>>> GetReadStatusAsync()
        {
            var readEvents = await _repository.GetAllAsync<HistoricalEvent>();

            var entityName = typeof(T2).FullName;

            var entities = await _repository.GetAllAsync<T1, T2>();
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
                .GetAllAsync<HistoricalEvent>()
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
            if (request.From == null) request.From = GetLastTimeViewed<T2>(id);
            if (request.To == null) request.To = DateTime.MaxValue;
            if (request.Mode == DeltaRequestModes.Snapshot) return await GetDeltaSnapshot(id, request.From.Value, request.To.Value);
            else if (request.Mode == DeltaRequestModes.Differential) return await GetDeltaDifferential(id, request.From.Value, request.To.Value);
            throw new NotImplementedException();
        }

        private async Task<object> GetDeltaSnapshot(T1 id, DateTime fromTimestamp, DateTime toTimestamp)
        {
            var events =
                from e in _repository.GetAll<HistoricalEvent>()
                join c in _repository.GetAllAsync<Guid, HistoricalChangeset>().Result on e.Id equals c.EventId
                where e.EntityId == id.ToString() && c.CreatedDate >= fromTimestamp && c.CreatedDate <= toTimestamp
                select e;

            if (!events.Any()) throw new NoHistoryException();

            var entity = await _repository.GetByIdAsync<T1, T2>(id);

            return ExtractSnapshotChanges(events, entity);
        }
        public async Task<object> GetDeltaDifferential(T1 id, DateTime fromTimestamp, DateTime toTimestamp)
        {
            // snapshot from creation date
            var events = _repository
                .GetAll<HistoricalEvent>()
                .Where(_ =>  _.EntityId == id.ToString() && _.CreatedDate >= fromTimestamp && _.CreatedDate <= toTimestamp && _.Action != HistoricalActions.Read.ToString())
                .OrderBy(_ => _.CreatedDate);

            if (events.Count() == 0) throw new NoHistoryException();
            //.Skip(1);

            var differentialChangeset = await ExtractDifferentialChangeset(id, events);

            return differentialChangeset;
        }

        private async Task<DifferentialChangeset> ExtractDifferentialChangeset(T1 id, IOrderedEnumerable<HistoricalEvent> events)
        {
            var changesets = await _repository.GetAllAsync<Guid, HistoricalChangeset>();
            var changeset = changesets.FirstOrDefault();
            var sourceEvent = events.First();
            var sourceObject = sourceEvent.Changeset.ObjectData == null ? JsonConvert.DeserializeObject<T2>(sourceEvent.Changeset.ObjectDelta) : JsonConvert.DeserializeObject<T2>(sourceEvent.Changeset.ObjectData);
            var differentialChangeset = new DifferentialChangeset();
            differentialChangeset.EntityId = id.ToString();
            differentialChangeset.EntityTypeName = sourceEvent.EntityName;
            differentialChangeset.Changesets = ExtractDifferentialChanges(id, events, sourceEvent, sourceObject);
            return differentialChangeset;
        }
        private List<Changeset> ExtractDifferentialChanges(T1 id, IOrderedEnumerable<HistoricalEvent> events, HistoricalEvent sourceEvent, T2 sourceObject)
        {

            var differentialChangeset = new List<Changeset>();

            var currentEvent = sourceEvent;
            var currentObject = sourceObject;
            for (int i = 1; i < events.Count(); i++)
            {
                var nextEvent = events.ToArray()[i];
                var nextEventObject = JsonConvert.DeserializeObject<T2>(currentEvent.Changeset.ObjectDelta);
                var changeset = new Changeset();
                changeset.EventDate = currentEvent.CreatedDate.Value;
                changeset.UserId = currentEvent.CreatedBy;
                changeset.ChangesetId = currentEvent.Changeset.Id;
                changeset.EventName = currentEvent.Action;
                changeset.Changes = ExtractChanges(currentObject, nextEventObject);
                differentialChangeset.Add(changeset);

                currentObject = nextEventObject;
                currentEvent = nextEvent;
            }
            // add last event to current object
            var lastChangeset = new Changeset();
            var lastEvent = events.Last();

            lastChangeset.EventDate = lastEvent.CreatedDate.Value;
            lastChangeset.UserId = lastEvent.CreatedBy;
            lastChangeset.ChangesetId = lastEvent.Changeset.Id;
            lastChangeset.Changes = lastEvent.Action != HistoricalActions.Delete.ToString() ? ExtractChanges(currentObject, _repository.GetById<T1, T2>(id)) : null;
            lastChangeset.EventName = lastEvent.Action;

            differentialChangeset.Add(lastChangeset);

            return differentialChangeset;
        }
        private SnapshotChangeset ExtractSnapshotChanges(IEnumerable<HistoricalEvent> events, T2 actual)
        {
            // base line compararer
            
            var sourceEvent = events.FirstOrDefault();
            //var sourceChangeset = _repository
            //    .GetAllAsync<Guid, HistoricalChangeset>()
            //    .Result
            //    .SingleOrDefault(_ => _.EventId == sourceEvent.Id);

            var sourceObject = sourceEvent.Changeset == null ?
                JsonConvert.DeserializeObject<T2>(sourceEvent.Changeset.ObjectData) :
                JsonConvert.DeserializeObject<T2>(sourceEvent.Changeset.ObjectDelta);

            var snapshotChangeset = new SnapshotChangeset();
            snapshotChangeset.EntityTypeName = sourceEvent.EntityName;
            snapshotChangeset.EntityId = sourceEvent.EntityId;
            snapshotChangeset.LastViewed = DateTime.Now; // TODO: Get Last Viewed Info from read status (if available)
            snapshotChangeset.LastModifiedBy = events.Last().CreatedBy;
            snapshotChangeset.LastModifiedEvent = events.Last().Action;
            snapshotChangeset.LastModifiedDate = events.Last().CreatedDate.Value;
            snapshotChangeset.Changes = ExtractChanges(sourceObject, actual);
            return snapshotChangeset;
        }

        private static List<Change> ExtractChanges<T>(T source, T destination)
        {
            var changes = new List<Change>();
            if (destination != null)
            {
                destination
                    .GetType()
                    .GetProperties()
                    .Where(_ => _.DeclaringType == destination.GetType() && !Attribute.IsDefined(_, typeof(IgnoreInChangesetAttribute)))
                    .ToList()
                    .ForEach(_ => changes.Add(new Change()
                    {
                        FieldName = _.Name,
                        FromValue = source.GetType().GetProperty(_.Name).GetValue(source),
                        ToValue = destination.GetType().GetProperty(_.Name).GetValue(destination)
                    }));
            }
            return changes;
        }

        private DateTime? GetLastTimeViewed<T2>(T1 id)
        {
            var lastView = _repository
                .GetAll<HistoricalEvent>()
                .SingleOrDefault(_ =>
                    _.EntityId == id.ToString() &&
                    _.EntityName == typeof(T2).FullName &&
                    _.Action == HistoricalActions.Read.ToString() &&
                    _.CreatedBy == _userInfoRepository.GetUserInfo());
            return lastView?.CreatedDate ?? DateTime.MinValue;
        }

        public virtual IEnumerable<T2> GetAll() => GetAllAsync().GetAwaiter().GetResult();

        public virtual T2 GetById(T1 id) => GetByIdAsync(id).GetAwaiter().GetResult();

        public virtual async Task<IEnumerable<T2>> GetAllAsync() => await _service.GetAllAsync();

        public virtual async Task<T2> GetByIdAsync(T1 id) => await _service.GetByIdAsync(id);
    }
}
