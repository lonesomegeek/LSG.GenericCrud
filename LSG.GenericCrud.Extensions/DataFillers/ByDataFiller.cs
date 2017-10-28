using System;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.DataFillers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LSG.GenericCrud.Extensions.DataFillers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="LSG.GenericCrud.DataFillers.IEntityDataFiller{LSG.GenericCrud.Models.BaseEntity}" />
    /// <seealso cref="BaseEntity" />
    public class ByDataFiller : IEntityDataFiller<BaseEntity>
    {
        /// <summary>
        /// The user information repository
        /// </summary>
        private readonly IUserInfoRepository _userInfoRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ByDataFiller"/> class.
        /// </summary>
        /// <param name="userInfoRepository">The user information repository.</param>
        public ByDataFiller(IUserInfoRepository userInfoRepository)
        {
            _userInfoRepository = userInfoRepository;
        }
        /// <summary>
        /// Determines whether [is entity supported] [the specified entry].
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns>
        ///   <c>true</c> if [is entity supported] [the specified entry]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEntitySupported(EntityEntry entry)
        {
            return entry.Entity is BaseEntity && (entry.State == EntityState.Added ||
                                                  entry.State == EntityState.Modified);
        }

        /// <summary>
        /// Fills the specified entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        public BaseEntity Fill(EntityEntry entry)
        {
            var entity = ((BaseEntity)entry.Entity);
            if (entry.State == EntityState.Added) entity.CreatedBy = _userInfoRepository?.GetUserInfo();
            entity.ModifiedBy = _userInfoRepository?.GetUserInfo();
            return entity;
        }

        /// <summary>
        /// Fills the specified entry async.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<BaseEntity> FillAsync(EntityEntry entry)
        {
            throw new NotImplementedException();
        }
    }
}