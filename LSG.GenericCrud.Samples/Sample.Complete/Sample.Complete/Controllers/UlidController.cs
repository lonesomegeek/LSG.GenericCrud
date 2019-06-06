using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using Sample.Complete.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Complete.Controllers
{
    [Route("api/[controller]")]
    public class UlidController : CrudController<string, MyUlid>
    {
        public UlidController(ICrudService<string, MyUlid> service) : base(service)
        {
        }
    }
}
