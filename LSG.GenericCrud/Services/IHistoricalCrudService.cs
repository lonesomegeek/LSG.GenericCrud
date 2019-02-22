using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;

namespace LSG.GenericCrud.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="LSG.GenericCrud.Services.ICrudService{T}" />
    public interface IHistoricalCrudService<T> : IHistoricalCrudService<Guid, T> { }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="LSG.GenericCrud.Services.ICrudService{T}" />
    public interface IHistoricalCrudService<T1, T2> : ICrudService<T1, T2>
    {
        /// <summary>
        /// Restores the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T2 Restore(T1 id);
        /// <summary>
        /// Gets the history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IEnumerable<IEntity> GetHistory(T1 id);
        /// <summary>
        /// Restores the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<T2> RestoreAsync(T1 id);
        /// <summary>
        /// Gets the history asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<IEnumerable<IEntity>> GetHistoryAsync(T1 id);

        Task<T2> CopyFromChangeset(T1 entityId, Guid changesetId);


    }
}
