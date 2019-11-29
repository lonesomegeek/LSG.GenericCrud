using System;
using Microsoft.AspNetCore.Http;

namespace Sample.GlobalFilters.Repositories
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserInfoRepository(/*IHttpContextAccessor httpContext*/)
        {
            //_httpContext = httpContext;
        }

        public Guid TenantId => Guid.NewGuid();//=> Guid.Parse(_httpContext.HttpContext.Session.GetString("TenantId"));
    }
}