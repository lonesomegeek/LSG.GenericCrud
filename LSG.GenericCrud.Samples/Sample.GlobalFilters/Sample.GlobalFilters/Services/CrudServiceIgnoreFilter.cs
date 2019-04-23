using System;
using System.Collections.Generic;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using Sample.GlobalFilters.Controllers;
using Sample.GlobalFilters.Repositories;

namespace Sample.GlobalFilters.Services
{
    public class CrudServiceIgnoreFilter<T1, T2> : CrudService<T1, T2> where T2 : class, IEntity<T1>, new()
    {
        private readonly ICrudRepository _repository;

        //public CrudServiceIgnoreFilter(ICrudRepository repository)
        //{
        //    _repository = repository;
        //}

        public IEnumerable<T2> GetAllIgnoreFilters()
        {
            return _repository.GetAllIgnoreFilter<T1, T2>();
        }
    }
}