using System;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LSG.GenericCrud.Repositories.DataFillers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="LSG.GenericCrud.Repositories.DataFillers.IEntityDataFiller{LSG.GenericCrud.Models.BaseEntity}" />
    public class ByDataFiller : IEntityDataFiller<BaseEntity>
    {
        private readonly IUserInfoRepository _userInfoRepository;

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

        public Task<BaseEntity> FillAsync(EntityEntry entry)
        {
            throw new NotImplementedException();
        }
    }
}