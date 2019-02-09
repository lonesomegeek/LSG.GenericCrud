using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricalEventsController : CrudController<HistoricalEvent>
    {
        public HistoricalEventsController(ICrudService<HistoricalEvent> service) : base(service)
        {
        }
    }
}
