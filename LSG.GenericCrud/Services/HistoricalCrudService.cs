using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.AspNetCore.Hosting.Internal;
using Newtonsoft.Json;

namespace LSG.GenericCrud.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="LSG.GenericCrud.Services.CrudService{T}" />
    /// <seealso cref="LSG.GenericCrud.Services.IHistoricalCrudService{T}" />
    public class HistoricalCrudService<T> : CrudService<T>, IHistoricalCrudService<T> where T : class, IEntity, new()
    {
        ///// <summary>
        ///// The event repository
        ///// </summary>
        //private readonly ICrudRepository<HistoricalEvent> _eventRepository;
        
        /// <summary>
        /// The repository
        /// </summary>
        private readonly ICrudRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoricalCrudService{T}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="eventRepository">The event repository.</param>
        public HistoricalCrudService(ICrudRepository repository) : base(repository)
        {
            _repository = repository;
            AutoCommit = false;
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public new T Create(T entity)
        {
            var createdEntity = base.Create(entity);

            var historicalEvent = new HistoricalEvent
            {
                Action = HistoricalActions.Create.ToString(),
                Changeset = new T().DetailedCompare(entity),
                EntityId = entity.Id,
                EntityName = entity.GetType().Name
            };

            _repository.Create(historicalEvent);
            _repository.SaveChanges();
            // TODO: Do I need to call the other repo for both repositories, or do I need a UoW (bugfix created)
            return createdEntity;
        }

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public new async Task<T> CreateAsync(T entity)
        {
            var createdEntity = await base.CreateAsync(entity);

            var historicalEvent = new HistoricalEvent
            {
                Action = HistoricalActions.Create.ToString(),
                Changeset = new T().DetailedCompare(entity),
                EntityId = entity.Id,
                EntityName = entity.GetType().Name
            };

            await _repository.CreateAsync(historicalEvent);
            _repository.SaveChanges();
            // TODO: Do I need to call the other repo for both repositories, or do I need a UoW (bugfix created)
            return createdEntity;
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public new T Update(Guid id, T entity)
        {
            var originalEntity = base.GetById(id);
            var historicalEvent = new HistoricalEvent
            {
                Action = HistoricalActions.Update.ToString(),
                Changeset = originalEntity.DetailedCompare(entity),
                EntityId = originalEntity.Id,
                EntityName = entity.GetType().Name
            };
            var modifiedEntity = base.Update(id, entity);

            _repository.Create(historicalEvent);
            _repository.SaveChanges();

            return modifiedEntity;
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public new async Task<T> UpdateAsync(Guid id, T entity)
        {
            var originalEntity = await base.GetByIdAsync(id);
            var historicalEvent = new HistoricalEvent
            {
                Action = HistoricalActions.Update.ToString(),
                Changeset = originalEntity.DetailedCompare(entity),
                EntityId = originalEntity.Id,
                EntityName = entity.GetType().Name
            };
            var modifiedEntity = await base.UpdateAsync(id, entity);

            await _repository.CreateAsync(historicalEvent);
            _repository.SaveChanges();

            return modifiedEntity;
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public new T Delete(Guid id)
        {
            var entity = base.Delete(id);

            // store all object in historical event
            var historicalEvent = new HistoricalEvent
            {
                Action = HistoricalActions.Delete.ToString(),
                Changeset = new T().DetailedCompare(entity),
                EntityId = entity.Id,
                EntityName = entity.GetType().Name
            };
            _repository.Create(historicalEvent);
            _repository.SaveChanges();

            return entity;
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public new async Task<T> DeleteAsync(Guid id)
        {
            var entity = await base.DeleteAsync(id);

            // store all object in historical event
            var historicalEvent = new HistoricalEvent
            {
                Action = HistoricalActions.Delete.ToString(),
                Changeset = new T().DetailedCompare(entity),
                EntityId = entity.Id,
                EntityName = entity.GetType().Name
            };
            await _repository.CreateAsync(historicalEvent);
            _repository.SaveChanges();

            return entity;
        }

        /// <summary>
        /// Restores the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="LSG.GenericCrud.Exceptions.EntityNotFoundException"></exception>
        public T Restore(Guid id)
        {
            var entity = _repository
                .GetAll<HistoricalEvent>()
                .SingleOrDefault(_ =>
                    _.EntityId == id &&
                    _.Action == HistoricalActions.Delete.ToString());
            if (entity == null) throw new EntityNotFoundException();
            var json = entity.Changeset;
            var obj = JsonConvert.DeserializeObject<T>(json);
            var createdObject = Create(obj);

            return createdObject;
        }

        /// <summary>
        /// Restores the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="LSG.GenericCrud.Exceptions.EntityNotFoundException"></exception>
        public async Task<T> RestoreAsync(Guid id)
        {
            var entity = _repository
                .GetAllAsync<HistoricalEvent>()
                .Result
                .SingleOrDefault(_ =>
                    _.EntityId == id &&
                    _.Action == HistoricalActions.Delete.ToString());
            if (entity == null) throw new EntityNotFoundException();
            var json = entity.Changeset;
            var obj = JsonConvert.DeserializeObject<T>(json);
            var createdObject = await CreateAsync(obj);

            return createdObject;
        }

        /// <summary>
        /// Gets the history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="LSG.GenericCrud.Exceptions.EntityNotFoundException"></exception>
        public IEnumerable<IEntity> GetHistory(Guid id)
        {
            var events = _repository
                .GetAll<HistoricalEvent>()
                .Where(_ => _.EntityId == id).ToList();
            if (!events.Any()) throw new EntityNotFoundException();
            return events;
        }

        /// <summary>
        /// Gets the history asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="LSG.GenericCrud.Exceptions.EntityNotFoundException"></exception>
        public async Task<IEnumerable<IEntity>> GetHistoryAsync(Guid id)
        {
            var events =  await _repository.GetAllAsync<HistoricalEvent>();
            var filteredEvents = events
                .Where(_ => _.EntityId == id)
                .ToList();
            if (!filteredEvents.Any()) throw new EntityNotFoundException();
            return filteredEvents;
        }
    }
}
