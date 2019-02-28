using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntController : HistoricalCrudController<int, MyIntEntity>
    {
        public IntController(ICrudController<int, MyIntEntity> crudController, IHistoricalCrudService<int, MyIntEntity> historicalCrudService) : base(crudController, historicalCrudService)
        {
        }
    }
}
