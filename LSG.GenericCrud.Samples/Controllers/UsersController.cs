﻿using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Samples.Models.Entities;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LSG.GenericCrud.Samples.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : HistoricalCrudControllerBase<Guid, User>
    {
        public UsersController(ICrudController<Guid, User> crudController, IHistoricalCrudService<Guid, User> historicalCrudService) : base(crudController, historicalCrudService) {}    
    }
}
