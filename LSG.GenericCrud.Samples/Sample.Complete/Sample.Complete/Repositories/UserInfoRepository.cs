using LSG.GenericCrud.Extensions.DataFillers;
using LSG.GenericCrud.Repositories;
using Microsoft.AspNetCore.Http;

namespace Sample.Complete.Repositories
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserInfoRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserInfo()
        {
            return "John Dude";
        }
    }
}
