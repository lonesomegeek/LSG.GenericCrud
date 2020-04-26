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
            services.AddScoped(typeof(ICrudController<,>), typeof(CrudControllerBase<,>));
            services.AddScoped(typeof(ICrudCopyController<,>), typeof(CrudControllerBase<,>));

            services.AddScoped(typeof(IHistoricalCrudController<,>), typeof(HistoricalCrudControllerBase<,>));
            services.AddScoped(typeof(IHistoricalCrudReadService<,>), typeof(HistoricalCrudControllerBase<,>));

            services.AddScoped(typeof(ICrudService<,>), typeof(CrudServiceBase<,>));
            services.AddScoped(typeof(ICrudRepository), typeof(CrudRepository));
            services.AddScoped(typeof(IHistoricalCrudService<,>), typeof(HistoricalCrudServiceBase<,>));
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
