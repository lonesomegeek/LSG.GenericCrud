using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.TestApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.TestApi.Controllers
{
    [Route("api/[controller]")]
    public class HistoricalItemsController : CrudController<Item>
    {
        public HistoricalItemsController(HistoricalCrudController<Item> dal) : base(dal) { }
    }
}
