using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;

namespace LSG.GenericCrud.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="LSG.GenericCrud.Services.ICrudService{T}" />
    public class CrudService<T> : ICrudService<T> where T : class, IEntity, new()
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
        public virtual IEnumerable<T> GetAll() => GetAllAsync().GetAwaiter().GetResult();

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _repository.GetAllAsync<T>();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="LSG.GenericCrud.Exceptions.EntityNotFoundException"></exception>
        public virtual T GetById(Guid id) => GetByIdAsync(id).GetAwaiter().GetResult();

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="LSG.GenericCrud.Exceptions.EntityNotFoundException"></exception>
        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync<T>(id);
            if (entity == null) throw new EntityNotFoundException();
            return entity;
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
            var createdEntity = await _repository.CreateAsync(entity);
            if (AutoCommit) await _repository.SaveChangesAsync();
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
            var originalEntity = await GetByIdAsync(id);
            foreach (var prop in entity.GetType().GetProperties())
            {
                if (prop.Name != "Id")
                {
                    var originalProperty = originalEntity.GetType().GetProperty(prop.Name);
                    var value = prop.GetValue(entity, null);
                    if (value != null) originalProperty.SetValue(originalEntity, value);
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
        public virtual T Delete(Guid id) => DeleteAsync(id).GetAwaiter().GetResult();

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual async Task<T> DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            await _repository.DeleteAsync<T>(id);
            if (AutoCommit) await _repository.SaveChangesAsync();
            return entity;
        }
    }
}