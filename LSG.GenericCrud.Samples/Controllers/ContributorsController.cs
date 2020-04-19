using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Samples.Models.Entities;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Samples.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContributorsController : CrudControllerBase<Guid, Contributor>
    {
        public ContributorsController(ICrudService<Guid, Contributor> service) : base(service) {}
    }
}
