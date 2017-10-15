using System;
using System.Collections.Generic;
using System.Text;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Services;

namespace LSG.GenericCrud.Controllers
{ 
    public class HistoricalCrudController<T> : CrudController<T> where T : class, IEntity, new()
    {
        public HistoricalCrudController(ICrudService<T> crudService, IHistoricalCrudService<T> historicalCrudService) : base(crudService)
        {
            
        }


    }
}
