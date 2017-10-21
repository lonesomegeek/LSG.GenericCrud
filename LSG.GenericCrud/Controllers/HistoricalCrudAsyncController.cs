using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.Controllers
{ 
    public class HistoricalCrudAsyncController<T> : CrudAsyncController<T> where T : class, IEntity, new()
    {
        private readonly IHistoricalCrudService<T> _historicalCrudService;

        public HistoricalCrudAsyncController(IHistoricalCrudService<T> historicalCrudService) : base(historicalCrudService)
        {
            _historicalCrudService = historicalCrudService;
        }

        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetHistory(Guid id)
        {
            try
            {
                return Ok(await _historicalCrudService.GetHistoryAsync(id));
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("{id}/restore")]
        public async Task<IActionResult> Restore(Guid id)
        {
            try
            {
                return Ok(await _historicalCrudService.RestoreAsync(id));
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
