using System;
using System.Threading.Tasks;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Sample.GlobalFilters.Models;
using Sample.GlobalFilters.Repositories;
using Sample.GlobalFilters.Services;

namespace Sample.GlobalFilters.Controllers
{
    [Route("api/[controller]")]
    public class CarsController : CrudController<Car>
    {
        private readonly IServiceProvider _serviceProvider;

        public CarsController(ICrudController<Guid, Car> controller, IServiceProvider serviceProvider) : base(controller)
        {
            _serviceProvider = serviceProvider;
        }

        [Route("all")]
        [HttpGet]
        public IActionResult GetAllIgnoreFilters()
        {
            var repository = new CrudRepositoryIgnoreFilter(
                _serviceProvider.GetService<IDbContext>(), 
                _serviceProvider.GetService<CrudRepository>());
            var service = new CrudServiceIgnoreFilter<Guid, Car>(
                _serviceProvider.GetService<ICrudService<Guid, Car>>(),
                repository);

            return Ok(service.GetAllIgnoreFilters());
        }
    }
}
