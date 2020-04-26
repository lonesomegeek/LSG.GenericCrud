using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Samples.Models.DTOs;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LSG.GenericCrud.Samples.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : HistoricalCrudControllerBase<Guid, UserDto>
    {
        public UsersController(ICrudController<Guid, UserDto> crudController, IHistoricalCrudService<Guid, UserDto> historicalCrudService) : base(crudController, historicalCrudService) {}    
    }
}
