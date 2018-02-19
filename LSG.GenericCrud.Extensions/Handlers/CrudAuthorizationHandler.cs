using System;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace LSG.GenericCrud.Extensions.Handlers
{
    public class CrudAuthorizationHandler : AuthorizationHandler<CrudRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CrudRequirement requirement)
        {
            var controllerType = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)((Microsoft.AspNetCore.Mvc.ActionContext)context.Resource).ActionDescriptor).ControllerTypeInfo;

            if (!typeof(ICrudAuthorizationOptions).IsAssignableFrom(controllerType)) return Task.CompletedTask;
            var controllerInstance = (ICrudAuthorizationOptions)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(controllerType);

            var resourceOptions = controllerInstance.Options;
            if (resourceOptions == null) return Task.CompletedTask;

            var verb = ((Microsoft.AspNetCore.Http.Internal.DefaultHttpRequest)((Microsoft.AspNetCore.Http.DefaultHttpContext)((Microsoft.AspNetCore.Mvc.ActionContext)context.Resource).HttpContext).Request).Method;
            var action = GetActionFromVerb(verb);

            if (!IsActionAvailable(action, resourceOptions)) context.Fail();
            else if (!IsActionProtected(action, resourceOptions)) context.Succeed(requirement);
            else
            {
                var resource = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)((Microsoft.AspNetCore.Mvc.ActionContext)context.Resource).ActionDescriptor).ControllerName.ToLower();
                var requiredSubscope = GetSubscopeFromAction(action, resourceOptions);
                var requiredScope = $"{resource}.{requiredSubscope}";
                var scopes = context.User.FindFirst("scope")?.Value.Split(' ');
                if (scopes != null && scopes.Contains(requiredScope)) context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }

        private string GetSubscopeFromAction(CrudActions action, CrudAuthorizationOptions options)
        {
            if (action == CrudActions.Create) return options.RequiredCreateSubscope;
            if (action == CrudActions.Read) return options.RequiredReadSubscope;
            if (action == CrudActions.Update) return options.RequiredUpdateSubscope;
            if (action == CrudActions.Delete) return options.RequiredDeleteSubscope;

            throw new NotSupportedException("Unsupported action.");
        }

        private bool IsActionProtected(CrudActions action, CrudAuthorizationOptions options)
        {
            if (action == CrudActions.Create) return options.IsCreateProtected;
            if (action == CrudActions.Read) return options.IsReadProtected;
            if (action == CrudActions.Update) return options.IsUpdateProtected;
            if (action == CrudActions.Delete) return options.IsDeleteProtected;

            throw new NotSupportedException("Unsupported action.");
        }

        private bool IsActionAvailable(CrudActions action, CrudAuthorizationOptions options)
        {
            if (action == CrudActions.Create) return options.IsCreateAvailable;
            if (action == CrudActions.Read) return options.IsReadAvailable;
            if (action == CrudActions.Update) return options.IsUpdateAvailable;
            if (action == CrudActions.Delete) return options.IsDeleteAvailable;

            throw new NotSupportedException("Unsupported action.");
        }

        private CrudActions GetActionFromVerb(string verb)
        {
            if (verb.Equals("POST", StringComparison.OrdinalIgnoreCase)) return CrudActions.Create;
            if (verb.Equals("GET", StringComparison.OrdinalIgnoreCase)) return CrudActions.Read;
            if (verb.Equals("PUT", StringComparison.OrdinalIgnoreCase)) return CrudActions.Update;
            if (verb.Equals("DELETE", StringComparison.OrdinalIgnoreCase)) return CrudActions.Delete;

            throw new NotSupportedException($"{verb} verb is not supported.");
        }
    }
}
