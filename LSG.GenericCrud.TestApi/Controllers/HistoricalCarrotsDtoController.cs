using AutoMapper;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.TestApi.Models;
using LSG.GenericCrud.TestApi.Models.DTOs;
using LSG.GenericCrud.TestApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.TestApi.Controllers
{
    [Route("api/[controller]")]
    public class HistoricalCarrotsDtoController : HistoricalCrudController<CarrotDto, Carrot>
    {
        public HistoricalCarrotsDtoController(HistoricalCrud<Carrot> dal, IMapper mapper) : base(dal, mapper) { }
    }
}
