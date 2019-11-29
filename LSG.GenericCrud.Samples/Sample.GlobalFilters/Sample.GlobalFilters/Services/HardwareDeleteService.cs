using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using Sample.GlobalFilters.Models;
using Sample.GlobalFilters.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.GlobalFilters.Services
{
    public class HardwareDeleteService<T1, T2> : 
        IHardwareDeleteService<T1,T2> 
        where T2 : 
            class, 
            IEntity<T1>, 
            IHardwareDelete, 
            new()
    {
        private readonly ICrudService<T1, T2> _service;
        private readonly ICrudRepository _repository;

        public HardwareDeleteService(
            ICrudService<T1, T2> service,
            ICrudRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        public async Task<T2> DeleteHardAsync(T1 id)
        {
            var entity = await _service.GetByIdAsync(id);
            entity.IsHardwareDelete = true;
            var deletedEntity = await _service.DeleteAsync(id);
            return deletedEntity;
        }

    }
}
