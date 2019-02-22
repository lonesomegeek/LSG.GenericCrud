using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Helpers;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;

namespace LSG.GenericCrud.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="LSG.GenericCrud.Services.ICrudService{T}" />
    public class CrudService<T> : ICrudService<Guid, T> where T : class, IEntity, new()
    {
        private readonly ICrudService<Guid, T> _service;

        public CrudService(ICrudService<Guid, T> service)
        {
            _service = service;
            AutoCommit = true;
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
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="LSG.GenericCrud.Services.ICrudService{T}" />
    public class CrudService<T1, T2> : ICrudService<T1, T2> where T2 : class, IEntity<T1>, new()
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly ICrudRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudService{T}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public CrudService(ICrudRepository repository)
        {
            _repository = repository;
            AutoCommit = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [automatic commit].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [automatic commit]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoCommit { get; set; }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T2> GetAll() => GetAllAsync().GetAwaiter().GetResult();

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T2>> GetAllAsync() => await _repository.GetAllAsync<T1, T2>();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="LSG.GenericCrud.Exceptions.EntityNotFoundException"></exception>
        public virtual T2 GetById(T1 id) => GetByIdAsync(id).GetAwaiter().GetResult();

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="LSG.GenericCrud.Exceptions.EntityNotFoundException"></exception>
        public virtual async Task<T2> GetByIdAsync(T1 id)
        {
            var entity = await _repository.GetByIdAsync<T1, T2>(id);
            if (entity == null) throw new EntityNotFoundException();
            return entity;
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
            var createdEntity = await _repository.CreateAsync<T1, T2>(entity);
            if (AutoCommit) await _repository.SaveChangesAsync();
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
            var originalEntity = await GetByIdAsync(id);
            foreach (var prop in entity.GetType().GetProperties().Where(_=>_.DeclaringType == typeof(T2)))
            {
                if (prop.Name != "Id")
                {
                    var originalProperty = originalEntity.GetType().GetProperty(prop.Name);
                    var value = prop.GetValue(entity, null);
                    /*if (value != null) */originalProperty.SetValue(originalEntity, value);
                }
            }
            if (AutoCommit) await _repository.SaveChangesAsync();
            return originalEntity;
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
            var entity = await GetByIdAsync(id);
            await _repository.DeleteAsync<T1, T2>(id);
            if (AutoCommit) await _repository.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T2> CopyAsync(T1 id)
        {
            var actualEntity = await _repository.GetByIdAsync<T1, T2>(id);
            var copiedEntity = actualEntity.CopyObject();
            var createdEntity = await _repository.CreateAsync<T1, T2>(copiedEntity);
            if (AutoCommit) await _repository.SaveChangesAsync();
            return createdEntity;
        }
    }
}