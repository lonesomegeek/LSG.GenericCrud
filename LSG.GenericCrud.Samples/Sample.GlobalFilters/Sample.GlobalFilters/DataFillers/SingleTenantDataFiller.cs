using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.DataFillers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sample.GlobalFilters.Models;
using Sample.GlobalFilters.Repositories;

namespace Sample.GlobalFilters.DataFillers
{
    public class SingleTenantDataFiller : IEntityDataFiller
    {
        private readonly IUserInfoRepository _userInfoRepository;

        public SingleTenantDataFiller(IUserInfoRepository userInfoRepository)
        {
            _userInfoRepository = userInfoRepository;
        }

        public bool IsEntitySupported(EntityEntry entry)
        {
            return entry.Entity is ISingleTenantEntity && entry.State == EntityState.Added;
        }

        public object Fill(EntityEntry entry)
        {
            var entity = (ISingleTenantEntity) entry.Entity;
            entity.TenantId = _userInfoRepository.TenantId;
            return entity;
        }

        public Task<object> FillAsync(EntityEntry entry)
        {
            return Task.Run(() => Fill(entry));
        }
    }
}
