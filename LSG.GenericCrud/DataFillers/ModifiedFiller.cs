
using System;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LSG.GenericCrud.DataFillers
{
    public class ModifiedFiller : IEntityDataFiller
    {
        private readonly IUserInfoRepository _userInfoRepository;

        public ModifiedFiller(IServiceProvider serviceProvider)
        {
            _userInfoRepository = serviceProvider.GetService(typeof(IUserInfoRepository)) as IUserInfoRepository;
        }

        public bool IsEntitySupported(EntityEntry entry) => entry.Entity is IModifiedInfo && entry.State == EntityState.Modified;

        public async Task<object> FillAsync(EntityEntry entry)
        {
            var entity = ((IModifiedInfo)entry.Entity);
            entity.ModifiedBy = _userInfoRepository?.GetUserInfo();
            entity.ModifiedDate = DateTime.Now;
            return entity;
        }
    }
}
