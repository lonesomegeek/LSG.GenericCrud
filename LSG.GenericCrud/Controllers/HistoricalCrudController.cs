using System;
using System.Linq;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LSG.GenericCrud.Controllers
{
    public class HistoricalCrudController<T> : CrudController<T> where T : class, IEntity, new()
    {
        private readonly HistoricalCrud<T> _historicalDal;

        public HistoricalCrudController(HistoricalCrud<T> dal) : base(dal)
        {
            _historicalDal = dal;
        }
        
        [HttpPost("{entityId}/restore")]
        public IActionResult Restore(Guid entityId/*, string entityName*/) => Ok(_historicalDal.Restore(entityId));//_historicalDal.Restore(entityId);
    }
}
