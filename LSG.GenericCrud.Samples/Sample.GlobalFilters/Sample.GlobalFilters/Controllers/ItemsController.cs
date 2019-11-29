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
    public class ItemsController : CrudController<Guid, Item>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHardwareDeleteService<Guid, Item> _hardwareDeleteService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ItemsController(
            ICrudService<Guid, Item> service,
            IHardwareDeleteService<Guid, Item> hardwareDeleteService,
            IServiceProvider serviceProvider,
            IHttpContextAccessor httpContextAccessor) : base(service)
        {
            _serviceProvider = serviceProvider;
            _hardwareDeleteService = hardwareDeleteService;
            _httpContextAccessor = httpContextAccessor;
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



        [HttpDelete("{id}")]
        public override async Task<ActionResult<Item>> Delete(Guid id)
        {
            if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("X-HardDelete", out StringValues value) && value == "true")
            {
                return await _hardwareDeleteService.DeleteHardAsync(id);
            }
            else
            {
                return await base.Delete(id);
            }
        }

        [HttpDelete("{id}/hard")]
        public async Task<ActionResult<Item>> DeleteHard(Guid id)
        {
            return await _hardwareDeleteService.DeleteHardAsync(id);
        }
    }
}
