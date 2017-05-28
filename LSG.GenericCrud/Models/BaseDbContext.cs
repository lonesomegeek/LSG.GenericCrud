using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

namespace LSG.GenericCrud.Models
{
    public class BaseDbContext : DbContext
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEnumerable<IEntityDataFiller<BaseEntity>> _dataFillers;

        public BaseDbContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options)
        {
            _serviceProvider = serviceProvider;
            _dataFillers = _serviceProvider.GetServices<IEntityDataFiller<BaseEntity>>();

        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                foreach (var dataFiller in _dataFillers)
                {
                    if (dataFiller.IsEntitySupported(entry)) dataFiller.Fill(entry);
                }
            }

            return base.SaveChanges();
        }
    }

    public interface IEntityDataFiller<T>
    {
        bool IsEntitySupported(EntityEntry entry);
        T Fill(EntityEntry entry);
    }

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

    public class ByDataFiller : IEntityDataFiller<BaseEntity>
    {
        public bool IsEntitySupported(EntityEntry entry)
        {
            return entry.Entity is BaseEntity && (entry.State == EntityState.Added ||
                                                  entry.State == EntityState.Modified);
        }

        public BaseEntity Fill(EntityEntry entry)
        {
            var entity = ((BaseEntity)entry.Entity);
            entity.CreatedBy = "Not set yet!...";
            entity.ModifiedBy = "Not set yet!...";
            return entity;
        }
    }
}
