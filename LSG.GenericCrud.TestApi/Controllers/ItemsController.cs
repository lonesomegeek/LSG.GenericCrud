using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.DAL;
using LSG.GenericCrud.TestApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.TestApi.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : CrudController<Item>
    {
        public ItemsController(IDbContext context) : base(context)
        {
        }
    }
}
