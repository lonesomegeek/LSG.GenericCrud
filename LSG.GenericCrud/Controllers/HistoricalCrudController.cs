using System;
using System.Collections.Generic;
using System.Text;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.Controllers
{ 
    public class HistoricalCrudController<T> : CrudController<T> where T : class, IEntity, new()
    {
        private readonly IHistoricalCrudService<T> _historicalCrudService;

        public HistoricalCrudController(IHistoricalCrudService<T> historicalCrudService) : base(historicalCrudService)
        {
            _historicalCrudService = historicalCrudService;
        }

        [HttpGet("{id}/history")]
        public IActionResult GetHistory(Guid id)
        {
            try
            {
                return Ok(_historicalCrudService.GetHistory(id));
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("{id}/restore")]
        public IActionResult Restore(Guid id)
        {
            try
            {
                return Ok(_historicalCrudService.Restore(id));
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
