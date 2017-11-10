using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LSG.GenericCrud.DataFillers;
using LSG.GenericCrud.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LSG.GenericCrud.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class BaseDbContext : DbContext
    {
        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// The data fillers
        /// </summary>
        private readonly IEnumerable<IEntityDataFiller<IEntity>> _dataFillers;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDbContext" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public BaseDbContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options)
        {
            _serviceProvider = serviceProvider;
            _dataFillers = _serviceProvider?.GetServices<IEntityDataFiller<IEntity>>();

        }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <returns>
        /// The number of state entries written to the database.
        /// </returns>
        /// <remarks>
        /// This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
        /// changes to entity instances before saving to the underlying database. This can be disabled via
        /// <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        /// </remarks>
        public override int SaveChanges()
        {
            if (_dataFillers != null)
            {
                var entries = ChangeTracker.Entries();
                foreach (var entry in entries)
                {
                    foreach (var dataFiller in _dataFillers)
                    {
                        if (dataFiller.IsEntitySupported(entry)) dataFiller.Fill(entry);
                    }
                }
            }
            return base.SaveChanges();
        }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            if (_dataFillers != null)
            {
                var entries = ChangeTracker.Entries();
                foreach (var entry in entries)
                {
                    foreach (var dataFiller in _dataFillers)
                    {
                        if (dataFiller.IsEntitySupported(entry)) await dataFiller.FillAsync(entry);
                    }
                }
            }
            return await base.SaveChangesAsync();
        }
    }
}
