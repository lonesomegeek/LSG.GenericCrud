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
        public virtual T Restore(Guid id) =>  _service.Restore(id);
        public virtual IEnumerable<IEntity> GetHistory(Guid id) => _service.GetHistory(id);
        public virtual async Task<T> RestoreAsync(Guid id) => await _service.RestoreAsync(id);
        public virtual async Task<IEnumerable<IEntity>> GetHistoryAsync(Guid id) => await _service.GetHistoryAsync(id);
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

        public bool AutoCommit { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoricalCrudService{T}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="eventRepository">The event repository.</param>
        public HistoricalCrudService(ICrudService<T1, T2> service, ICrudRepository repository)
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
                Changeset = new T2().DetailedCompare(entity),
                EntityId = entity.Id.ToString(), // TODO: I do not like the string value compare here
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
                Changeset = originalEntity.DetailedCompare(entity),
                EntityId = originalEntity.Id.ToString(), // TODO: I do not like the string value compare here
                EntityName = entity.GetType().Name
            };
            var modifiedEntity = await _service.UpdateAsync(id, entity);

            await _repository.CreateAsync(historicalEvent);
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
                Changeset = new T2().DetailedCompare(entity),
                EntityId = entity.Id.ToString(), // TODO: I do not like the string value compare here
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
                .GetAllAsync<HistoricalEvent>()
                .Result
                .SingleOrDefault(_ =>
                    _.EntityId == id.ToString() && // TODO: I do not like the string value compare here
                    _.Action == HistoricalActions.Delete.ToString());
            if (entity == null) throw new EntityNotFoundException();
            var json = entity.Changeset;
            var obj = JsonConvert.DeserializeObject<T2>(json);
            var createdObject = await CreateAsync(obj);

            return createdObject;
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
            var events = await _repository.GetAllAsync<HistoricalEvent>();
            var filteredEvents = events
                .Where(_ => _.EntityId == id.ToString()) // TODO: I do not like the string value compare here
                .ToList();
            if (!filteredEvents.Any()) throw new EntityNotFoundException();
            return filteredEvents;
        }

        public virtual IEnumerable<T2> GetAll() => GetAllAsync().GetAwaiter().GetResult();

        public virtual T2 GetById(T1 id) => GetByIdAsync(id).GetAwaiter().GetResult();

        public virtual async Task<IEnumerable<T2>> GetAllAsync() => await _service.GetAllAsync();

        public virtual async Task<T2> GetByIdAsync(T1 id) => await _service.GetByIdAsync(id);
    }
}
