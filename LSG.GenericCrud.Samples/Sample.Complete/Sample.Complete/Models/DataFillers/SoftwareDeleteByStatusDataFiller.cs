using LSG.GenericCrud.DataFillers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Complete.Models.DataFillers
{
    public class SoftwareDeleteByStatusDataFiller : IEntityDataFiller
    {
        public object Fill(EntityEntry entry)
        {
            entry.State = EntityState.Modified;
            return entry.Entity;
        }

        public Task<object> FillAsync(EntityEntry entry)
        {
            return Task.Run(() => Fill(entry));
        }

        public bool IsEntitySupported(EntityEntry entry)
        {
            return                
                entry.Entity is IStatusable &&
                ((IStatusable)entry.Entity).Status == "Deleted" &&
                entry.State == EntityState.Deleted;
        }
    }
}
