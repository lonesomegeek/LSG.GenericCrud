using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LSG.GenericCrud.Extensions.Middlewares
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorizationMiddleware
    {
        /// <summary>
        /// The next
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationMiddleware" /> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            var scopes = Enumerable.FirstOrDefault<Claim>(context.User.Claims, _ => _.Type == "scope")?.Value.Split(' ');
            
            //if (scopes == null)
            //{
            //    ForbiddenAccess(context);
            //    return;
            //}

            //// is god!
            //if (scopes.Count(_ => _ == "all:all") == 1) await _next.Invoke(context);
            //// is managing the resource?
            //else if (scopes.Count(_ => _ == $"all:{context.Request.Path.Value.Split('/')[2]}") == 1) await _next.Invoke(context);
            //// have the required scope for the ressource
            //else if (scopes.Count(_ => _ == GetRequiredScope(context)) >= 1) await _next.Invoke(context);
            //// else... no access
            //else ForbiddenAccess(context);

            if (scopes != null && scopes.Count(_ => _ == GetRequiredScope(context)) >= 1) await _next.Invoke(context);
            else ForbiddenAccess(context);

        }

        /// <summary>
        /// Forbiddens the access.
        /// </summary>
        /// <param name="context">The context.</param>
        private async void ForbiddenAccess(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            await context.Response.WriteAsync("Access to the ressource is forbidden.");
        }

        /// <summary>
        /// Gets the required scope.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        private string GetRequiredScope(HttpContext context)
        {
            var method = context.Request.Method;
            var ressourceRequested = context.Request.Path.Value.Split('/')[2];
            if (HttpMethods.IsGet(method)) return $"read:{ressourceRequested}";
            if (HttpMethods.IsPost(method)) return $"create:{ressourceRequested}";
            if (HttpMethods.IsPut(method)) return $"update:{ressourceRequested}";
            if (HttpMethods.IsDelete(method)) return $"delete:{ressourceRequested}";
            return String.Empty;
        }
    }
}
