using LSG.GenericCrud.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Samples.Models
{
    public class UserInfoRepository : IUserInfoRepository
    {
        public string GetUserInfo()
        {
            return "na";
        }
    }
}
