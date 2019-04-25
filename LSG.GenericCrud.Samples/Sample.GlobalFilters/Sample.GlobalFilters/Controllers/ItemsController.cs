using System;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Sample.GlobalFilters.Models;
using Sample.GlobalFilters.Repositories;
using Sample.GlobalFilters.Services;

namespace Sample.GlobalFilters.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : CrudController<Item>
    {
        private readonly IServiceProvider _serviceProvider;

        public ItemsController(ICrudController<Guid, Item> controller, IServiceProvider serviceProvider) : base(controller)
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
            var service = new CrudServiceIgnoreFilter<Guid, Item>(
                _serviceProvider.GetService<ICrudService<Guid, Item>>(),
                repository);

            return Ok(service.GetAllIgnoreFilters());
        }
    }
}
