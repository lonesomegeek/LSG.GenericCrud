using System;
using System.Collections.Generic;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories.DataFillers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LSG.GenericCrud.Repositories
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
}
