using LSG.GenericCrud.Samples.OData.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Samples.OData.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AccountsController : ControllerBase
    {
        public IEnumerable<Account> GetAll()
        {
            return new List<Account>
            {
                new Account { Id = Guid.NewGuid(), Name = "Test1" },
                new Account { Id = Guid.NewGuid(), Name = "Test2" },
            };
        }
    }
}
