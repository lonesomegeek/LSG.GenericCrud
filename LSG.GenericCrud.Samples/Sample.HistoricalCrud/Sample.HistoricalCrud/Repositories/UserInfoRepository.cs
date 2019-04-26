using LSG.GenericCrud.Extensions.DataFillers;
using LSG.GenericCrud.Repositories;
using Microsoft.AspNetCore.Http;

namespace Sample.HistoricalCrud.Repositories
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
            return "John Dude";
        }
    }
}
