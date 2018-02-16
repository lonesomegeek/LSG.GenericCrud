using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Models;
using Microsoft.AspNetCore.Mvc;
using LSG.GenericCrud.Services;

namespace Sample.Complete.Controllers
{
    [Route("api/[controller]")]

    public class HistoricalEventsController : CrudController<HistoricalEvent>
    {
        public HistoricalEventsController(ICrudService<HistoricalEvent> service) : base(service)
        {
        }
    }
}
