using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.TestApi.Controllers
{
    [Route("api/[controller]")]

    public class HistoricalEventsController : CrudController<HistoricalEvent>
    {
        public HistoricalEventsController(Crud<HistoricalEvent> dal) : base(dal)
        {
        }
    }
}
