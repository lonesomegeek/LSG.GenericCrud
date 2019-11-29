using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.DataFillers;
using LSG.GenericCrud.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sample.GlobalFilters.Models;

namespace Sample.GlobalFilters.DataFillers
{
    public class SoftwareDeleteDataFiller : IEntityDataFiller
    {
        public object Fill(EntityEntry entry)
        {
            var entity = ((ISoftwareDelete)entry.Entity);
            if (entity is IHardwareDelete && (((IHardwareDelete)entity).IsHardwareDelete))
            {
                return entity;    
            } else
            {
                entity.IsDeleted = true;
                entry.State = EntityState.Modified; // be sure to not let the deleted flag, in other case, the entry will be deleted from database on save
                return entity;
            }            
        }

        public Task<object> FillAsync(EntityEntry entry)
        {
            return Task.Run(() => Fill(entry));
        }

        public bool IsEntitySupported(EntityEntry entry)
        {
            return entry.Entity is ISoftwareDelete && entry.State == EntityState.Deleted;
        }
    }
}
