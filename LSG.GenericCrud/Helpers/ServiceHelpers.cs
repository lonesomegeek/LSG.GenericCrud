using System;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LSG.GenericCrud.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceHelpers
    {
        /// <summary>
        /// Adds the crud.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddCrud(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICrudController<>), typeof(CrudController<>));
            services.AddScoped(typeof(ICrudCopyController<>), typeof(CrudController<>));
            services.AddScoped(typeof(ICrudController<,>), typeof(CrudController<,>));
            services.AddScoped(typeof(ICrudCopyController<,>), typeof(CrudController<,>));

            services.AddScoped(typeof(IHistoricalCrudController<>), typeof(HistoricalCrudController<>));
            services.AddScoped(typeof(IHistoricalCrudController<,>), typeof(HistoricalCrudController<,>));
            services.AddScoped(typeof(IHistoricalCrudRestoreController<>), typeof(HistoricalCrudController<>));
            services.AddScoped(typeof(IHistoricalCrudCopyController<>), typeof(HistoricalCrudController<>));
            services.AddScoped(typeof(IHistoricalCrudDeltaController<>), typeof(HistoricalCrudController<>));
            services.AddScoped(typeof(IHistoricalCrudReadStatusController<>), typeof(HistoricalCrudController<>));
            services.AddScoped(typeof(IHistoricalCrudReadService<,>), typeof(HistoricalCrudReadService<,>));

            services.AddScoped(typeof(ICrudService<>), typeof(CrudService<>));
            services.AddScoped(typeof(ICrudService<,>), typeof(CrudService<,>));
            services.AddScoped(typeof(ICrudRepository), typeof(CrudRepository));
            services.AddScoped(typeof(IHistoricalCrudService<>), typeof(HistoricalCrudService<>));
            services.AddScoped(typeof(IHistoricalCrudService<,>), typeof(HistoricalCrudService<,>));
            
            
        }
    }
}
