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
    public class HistoricalCrudService<T> : 
        ICrudService<T>,
        IHistoricalCrudService<T> where T : class, IEntity, new()
    {
        private readonly ICrudService<T> _service;

        /// <summary>
        /// The repository
        /// </summary>
        private readonly ICrudRepository _repository;

        public bool AutoCommit { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoricalCrudService{T}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="eventRepository">The event repository.</param>
        public HistoricalCrudService(ICrudService<T> service, ICrudRepository repository)
        {
            _service = service;
            _repository = repository;
            _service.AutoCommit = false;
            AutoCommit = false;
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual T Create(T entity) => CreateAsync(entity).GetAwaiter().GetResult();

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<T> CreateAsync(T entity)
        {
            var createdEntity = await _service.CreateAsync(entity);

            var historicalEvent = new HistoricalEvent
            {
                Action = HistoricalActions.Create.ToString(),
                Changeset = new T().DetailedCompare(entity),
                EntityId = entity.Id,
                EntityName = entity.GetType().Name
            };

            await _repository.CreateAsync(historicalEvent);
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
        public virtual T Update(Guid id, T entity) => UpdateAsync(id, entity).GetAwaiter().GetResult();

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<T> UpdateAsync(Guid id, T entity)
        {
            var originalEntity = await _service.GetByIdAsync(id);
            var originalEntitySerialized = JsonConvert.SerializeObject(originalEntity);
            var changeset = originalEntity.DetailedCompare(entity); // important to have it placed there because the update changes the originalEntity values...
            var modifiedEntity = await _service.UpdateAsync(id, entity);
            var historicalEvent = new HistoricalEvent
            {
                Action = HistoricalActions.Update.ToString(),
                Changeset = changeset,
                OriginalObject = originalEntitySerialized,
                EntityId = originalEntity.Id,
                EntityName = entity.GetType().Name
            };

            await _repository.CreateAsync(historicalEvent);
            await _repository.SaveChangesAsync();

            return modifiedEntity;
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual T Delete(Guid id) => DeleteAsync(id).GetAwaiter().GetResult();

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual async Task<T> DeleteAsync(Guid id)
        {
            var entity = await _service.DeleteAsync(id);

            // store all object in historical event
            var historicalEvent = new HistoricalEvent
            {
                Action = HistoricalActions.Delete.ToString(),
                Changeset = new T().DetailedCompare(entity),
                EntityId = entity.Id,
                EntityName = entity.GetType().Name
            };
            await _repository.CreateAsync(historicalEvent);
            await _repository.SaveChangesAsync();

            return entity;
        }

        /// <summary>
        /// Restores the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="LSG.GenericCrud.Exceptions.EntityNotFoundException"></exception>
        public virtual T Restore(Guid id) => RestoreAsync(id).GetAwaiter().GetResult();

        /// <summary>
        /// Restores the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="LSG.GenericCrud.Exceptions.EntityNotFoundException"></exception>
        public virtual async Task<T> RestoreAsync(Guid id)
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
        public virtual IEnumerable<IEntity> GetHistory(Guid id) => GetHistoryAsync(id).GetAwaiter().GetResult();

        /// <summary>
        /// Gets the history asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="LSG.GenericCrud.Exceptions.EntityNotFoundException"></exception>
        public virtual async Task<IEnumerable<IEntity>> GetHistoryAsync(Guid id)
        {
            var events =  await _repository.GetAllAsync<HistoricalEvent>();
            var filteredEvents = events
                .Where(_ => _.EntityId == id)
                .ToList();
            if (!filteredEvents.Any()) throw new EntityNotFoundException();
            return filteredEvents;
        }

        public virtual IEnumerable<T> GetAll() => GetAllAsync().GetAwaiter().GetResult();

        public virtual T GetById(Guid id) => GetByIdAsync(id).GetAwaiter().GetResult();

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _service.GetAllAsync();

        public virtual async Task<T> GetByIdAsync(Guid id) => await _service.GetByIdAsync(id);
    }
}
