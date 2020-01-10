
using System;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LSG.GenericCrud.DataFillers
{
    public class CreatedFiller : IEntityDataFiller
    {
        private readonly IUserInfoRepository _userInfoRepository;

        public CreatedFiller(IServiceProvider serviceProvider)
        {
            _userInfoRepository = serviceProvider.GetService(typeof(IUserInfoRepository)) as IUserInfoRepository;
        }

        public bool IsEntitySupported(EntityEntry entry) => entry.Entity is ICreatedInfo && entry.State == EntityState.Added;

        public async Task<object> FillAsync(EntityEntry entry)
        {
            var entity = ((ICreatedInfo)entry.Entity);
            entity.CreatedBy = _userInfoRepository?.GetUserInfo();
            entity.CreatedDate = DateTime.Now;
            return entity;
        }
    }
}
