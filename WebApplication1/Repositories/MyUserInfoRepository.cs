using LSG.GenericCrud.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Repositories
{
    public class MyUserInfoRepository : LSG.GenericCrud.Extensions.DataFillers.IUserInfoRepository
    {

        public string GetUserInfo()
        {
            return Guid.Parse("96268975-6BDB-4607-8D30-6219598E3E6D").ToString();
        }
    }
}
