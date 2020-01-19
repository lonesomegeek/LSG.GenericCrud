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
    public interface IHistoricalCrudService<T1, T2> : ICrudService<T1, T2> where T2 : class, IEntity<T1>, new()
    {   /// <summary>
        /// Restores the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<T2> RestoreAsync(T1 id);
        /// <summary>
        /// Restores the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<T2> RestoreFromChangeset(T1 entityId, Guid changesetId);
        /// <summary>
        /// Gets the history asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<IEnumerable<IEntity>> GetHistoryAsync(T1 id);

        Task<T2> CopyFromChangeset(T1 entityId, Guid changesetId);

        Task MarkAllAsRead();

        Task MarkAllAsUnread();

        Task MarkOneAsRead(T1 id);

        Task MarkOneAsUnread(T1 id);


        Task<IEnumerable<ReadeableStatus<T2>>> GetReadStatusAsync();
        Task<ReadeableStatus<T2>> GetReadStatusByIdAsync(T1 id);

        Task<object> Delta(T1 id, DeltaRequest request);
    }
}
