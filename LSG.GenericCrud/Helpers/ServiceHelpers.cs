using System;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
            services.AddScoped(typeof(ICrudController<,>), typeof(CrudController<,>));
            services.AddScoped(typeof(ICrudCopyController<,>), typeof(CrudController<,>));

            services.AddScoped(typeof(IHistoricalCrudController<,>), typeof(HistoricalCrudController<,>));
            services.AddScoped(typeof(IHistoricalCrudReadService<,>), typeof(HistoricalCrudReadService<,>));

            services.AddScoped(typeof(ICrudService<,>), typeof(CrudService<,>));
            services.AddScoped(typeof(ICrudRepository), typeof(CrudRepository));
            services.AddScoped(typeof(IHistoricalCrudService<,>), typeof(HistoricalCrudService<,>));
        }
        public static void AddCrudService(this IServiceCollection services)
        {
            AddCrudService(services, (options) =>
            {
                options.ShowMyNewStuff = false;
            });
        }
        public static void AddCrudService(this IServiceCollection services, Action<HistoricalCrudServiceOptions> options) 
        {
            services.Configure(options);
            
        }

    }
}
