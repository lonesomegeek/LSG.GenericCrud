using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LSG.GenericCrud.Repositories.DataFillers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityDataFiller<T>
    {
        /// <summary>
        /// Determines whether [is entity supported] [the specified entry].
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns>
        ///   <c>true</c> if [is entity supported] [the specified entry]; otherwise, <c>false</c>.
        /// </returns>
        bool IsEntitySupported(EntityEntry entry);
        /// <summary>
        /// Fills the specified entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        T Fill(EntityEntry entry);
    }
}