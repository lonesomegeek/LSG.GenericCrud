using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LSG.GenericCrud.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var scopes = context.User.Claims.FirstOrDefault(_ => _.Type == "scope")?.Value.Split(' ');

            var ressourceRequested = context.Request.Path.Value.Split('/')[2];
            var roleRequired = context.Request.Method == "GET" ? "read" : "manage";
            var scopeRequired = string.Format("{0}:{1}", roleRequired, ressourceRequested);
            if (scopes.Count(_ => _ == scopeRequired) >= 1)
            {
                await _next.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = (int) HttpStatusCode.Forbidden;
                await context.Response.WriteAsync("Missing scope");
                return;
            }

        }
    }
}
