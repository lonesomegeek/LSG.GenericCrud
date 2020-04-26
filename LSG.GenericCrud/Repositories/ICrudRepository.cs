using System;
using System.Collections.Generic;
using System.Linq;
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
        IQueryable<T2> GetAll<T1, T2>() where T2 : class, IEntity<T1>, new();

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<T2>> GetAllAsync<T1, T2>() where T2 : class, IEntity<T1>, new();

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
        Task<T2> GetByIdAsync<T1, T2>(T1 id) where T2 : class, IEntity<T1>, new();

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<T2> CreateAsync<T1, T2>(T2 entity) where T2 : class, IEntity<T1>, new();

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
        Task<T2> DeleteAsync<T1, T2>(T1 id) where T2 : class, IEntity<T1>, new();

        /// <summary>
        /// Saves the changes asyncronous.
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
    }
}