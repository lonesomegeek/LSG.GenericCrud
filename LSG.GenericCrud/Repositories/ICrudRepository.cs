using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;

namespace LSG.GenericCrud.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICrudRepository
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll<T>() where T : class, IEntity, new();

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync<T>() where T : class, IEntity, new();

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T2>> GetAllAsync<T1, T2>() where T2 : class, IEntity<T1>, new();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T GetById<T>(Guid id) where T : class, IEntity, new();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T2 GetById<T1, T2>(T1 id) where T2 : class, IEntity<T1>, new();

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<T> GetByIdAsync<T>(Guid id) where T : class, IEntity, new();

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<T2> GetByIdAsync<T1, T2>(T1 id) where T2 : class, IEntity<T1>, new();

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        T Create<T>(T entity) where T : class, IEntity, new();
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<T> CreateAsync<T>(T entity) where T : class, IEntity, new();

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<T2> CreateAsync<T1, T2>(T2 entity) where T2 : class, IEntity<T1>, new();

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        T Update<T>(Guid id, T entity) where T : class, IEntity, new();

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        T2 Update<T1, T2>(T1 id, T2 entity) where T2 : class, IEntity<T1>, new();

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<T> UpdateAsync<T>(Guid id, T entity) where T : class, IEntity, new();

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<T2> UpdateAsync<T1, T2>(T1 id, T2 entity) where T2 : class, IEntity<T1>, new();

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T Delete<T>(Guid id) where T : class, IEntity, new();

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T2 Delete<T1, T2>(T1 id) where T2 : class, IEntity<T1>, new();

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<T> DeleteAsync<T>(Guid id) where T : class, IEntity, new();

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<T2> DeleteAsync<T1, T2>(T1 id) where T2 : class, IEntity<T1>, new();

        /// <summary>
        /// Saves the changes.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Saves the changes asyncronous.
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
    }
}