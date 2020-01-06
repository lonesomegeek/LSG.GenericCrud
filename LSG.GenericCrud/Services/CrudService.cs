using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
            var properties = entity
                .GetType()
                .GetProperties()
                .Where(_ =>
                    _.DeclaringType == typeof(T2) && !Attribute.IsDefined(_, typeof(IgnoreInChangesetAttribute)));

            foreach (var prop in properties)
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
            if (actualEntity == null) throw new EntityNotFoundException();

            var copiedEntity = actualEntity.CopyObject();
            var createdEntity = await _repository.CreateAsync<T1, T2>(copiedEntity);
            if (AutoCommit) await _repository.SaveChangesAsync();

            return createdEntity;
        }
    }
}