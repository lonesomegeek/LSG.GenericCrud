using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.TestApi.Models;
using LSG.GenericCrud.TestApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.TestApi.Controllers
{
    [Route("api/[controller]")]
    public class HistoricalCarrotsController : HistoricalCrudController<Carrot>
    {
        public HistoricalCarrotsController(HistoricalCrud<Carrot> dal) : base(dal) { }
    }
}
