using System;
using LSG.GenericCrud.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LSG.GenericCrud.Repositories.DataFillers
{
    public class DateDataFiller : IEntityDataFiller<BaseEntity>
    {
        public bool IsEntitySupported(EntityEntry entry)
        {
            return entry.Entity is BaseEntity && (entry.State == EntityState.Added ||
                                                  entry.State == EntityState.Modified);
        }

        public BaseEntity Fill(EntityEntry entry)
        {
            if (entry.State == EntityState.Added) ((BaseEntity)entry.Entity).CreatedDate = DateTime.Now;
            ((BaseEntity)entry.Entity).ModifiedDate = DateTime.Now;
            return (BaseEntity)entry.Entity;
        }
    }
}