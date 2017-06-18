using LSG.GenericCrud.Repositories;
using Microsoft.AspNetCore.Http;

namespace LSG.GenericCrud.TestApi.Repositories
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
