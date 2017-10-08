using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace LSG.GenericCrud.Middlwares
{
    //public class CrudMiddleware
    //{
    //    private readonly RequestDelegate _next;
    //    private readonly IOptions<CrudOptions> _options;

    //    public CrudMiddleware(RequestDelegate next, IOptions<CrudOptions> options)
    //    {
    //        _next = next;
    //        _options = options;
    //    }

    //    public Task Invoke(HttpContext httpContext)
    //    {

    //        httpContext.Response.Headers.Add("X-Xss-Protection", "1");
    //        httpContext.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
    //        httpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    //        return _next(httpContext);
    //    }
    //}

    public class CrudOptions<T> : ICrudOptions<T>
    {
        public bool IsGetRouteEnabled { get; set; }
        public bool IsGetAllRouteEnabled { get; set; }
        public bool IsPostRouteEnabled { get; set; }
        public bool IsPutRouteEnabled { get; set; }
        public bool IsDeleteRouteEnabled { get; set; }
    }
   
    //public static class CrudMiddlewareExtensions
    //{
    //    public static IApplicationBuilder UseCrud(this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<CrudMiddleware>();
    //    }

    //    public static IApplicationBuilder UseCrud(this IApplicationBuilder builder, IOptions<CrudOptions> options)
    //    {
    //        return builder.UseMiddleware<CrudMiddleware>(options);
    //    }
    //}
}
