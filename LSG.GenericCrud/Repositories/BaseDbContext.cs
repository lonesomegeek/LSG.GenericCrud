using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using LSG.GenericCrud.DataFillers;
using LSG.GenericCrud.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;

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
        private readonly IEnumerable<IEntityDataFiller<BaseEntity>> _dataFillers;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public BaseDbContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options)
        {
            _serviceProvider = serviceProvider;
            _dataFillers = _serviceProvider?.GetServices<IEntityDataFiller<BaseEntity>>();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var type in GetEntityTypes())
            {
                var method = SetGlobalQueryMethod.MakeGenericMethod(type);
                method.Invoke(this, new object[] {modelBuilder});
            }

            base.OnModelCreating(modelBuilder);
        }

        private static IList<Type> _entityTypeCache;

        private static IList<Type> GetEntityTypes()
        {
            if (_entityTypeCache != null)
            {
                return _entityTypeCache.ToList();
            }

            _entityTypeCache = (from a in GetReferencingAssemblies()
                from t in a.DefinedTypes
                where typeof(ISoftDelete).IsAssignableFrom(t) && !t.IsAbstract
                select t.AsType()).ToList();

            return _entityTypeCache;
        }

        private static IEnumerable<Assembly> GetReferencingAssemblies()
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;

            foreach (var library in dependencies)
            {
                try
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
                catch (FileNotFoundException)
                { }
            }
            return assemblies;
        }

        static readonly MethodInfo SetGlobalQueryMethod = typeof(BaseDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance).Single(t => t.IsGenericMethod && t.Name == "SetGlobalQuery");


        public void SetGlobalQuery<T>(ModelBuilder builder) where T : BaseEntity
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
            {
                builder.Entity<T>().HasQueryFilter(_ => !((ISoftDelete)_).IsDeleted ?? false);

            }

            //builder.Entity<T>().HasKey(e => e.Id);
            ////Debug.WriteLine("Adding global query for: " + typeof(T));
            //builder.Entity<T>().HasQueryFilter(e => e.TenantId == _tenantId && !e.IsDeleted);
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
