using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using NUlid;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UlidController : CrudController<string, MyUlidEntity>
    {
        public UlidController(ICrudService<string, MyUlidEntity> service) : base(service)
        {
        }
    }
}
