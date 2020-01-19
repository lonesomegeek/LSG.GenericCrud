using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IEnumerable<IEntityDataFiller> _dataFillers;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDbContext" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public BaseDbContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options)
        {
            _serviceProvider = serviceProvider;
            _dataFillers = _serviceProvider?.GetServices<IEntityDataFiller>();
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
