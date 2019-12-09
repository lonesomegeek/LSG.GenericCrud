using LSG.GenericCrud.Extensions.DataFillers;
using LSG.GenericCrud.Repositories;
using Microsoft.AspNetCore.Http;

namespace Sample.Complete.Repositories
{
    public class UserInfoRepository : LSG.GenericCrud.Services.IUserInfoRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserInfoRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserInfo()
        {
            return "3893d165-3fd1-4071-8c22-0c02fb8fb05a";
        }
    }
}
