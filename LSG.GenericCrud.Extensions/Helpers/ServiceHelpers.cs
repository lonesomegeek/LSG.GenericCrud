using LSG.GenericCrud.Extensions.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace LSG.GenericCrud.Extensions.Helpers
{
    public static class ServiceHelpers
    {
        public static void AddCrudAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CrudDefaultHandler", policy => policy.Requirements.Add(new CrudRequirement()));
            });
            services.AddSingleton<IAuthorizationHandler, CrudAuthorizationHandler>();
        }
    }
}
