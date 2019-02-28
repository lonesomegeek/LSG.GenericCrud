using LSG.GenericCrud.Dto.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LSG.GenericCrud.Dto.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceHelpers
    {
        /// <summary>
        /// Adds the crud dto.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddCrudDto(this IServiceCollection services)
        {
            services.AddScoped(typeof(CrudService<,,>));
            services.AddScoped(typeof(HistoricalCrudService<,,>));
        }
    }
}
