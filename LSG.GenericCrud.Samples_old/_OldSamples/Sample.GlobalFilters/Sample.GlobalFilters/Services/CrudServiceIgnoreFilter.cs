using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using Sample.GlobalFilters.Controllers;
using Sample.GlobalFilters.Repositories;

namespace Sample.GlobalFilters.Services
{
    public class CrudServiceIgnoreFilter<T1, T2> : ICrudService<T1, T2> where T2 : class, IEntity<T1>, new()
    {
        private readonly CrudRepositoryIgnoreFilter _repository;
        private readonly ICrudService<T1, T2> _service;

        public CrudServiceIgnoreFilter(ICrudService<T1, T2> service, CrudRepositoryIgnoreFilter repository)
        {
            _service = service;
            _repository = repository;
        }

        public bool AutoCommit { get; set; }

        public async Task<T2> CopyAsync(T1 id) => await _service.CopyAsync(id);
        public T2 Create(T2 entity) => _service.Create(entity);
        public async Task<T2> CreateAsync(T2 entity) => await _service.CreateAsync(entity);
        public T2 Delete(T1 id) => _service.Delete(id);
        public async Task<T2> DeleteAsync(T1 id) => await _service.DeleteAsync(id);
        public IEnumerable<T2> GetAll() => _service.GetAll();
        public async Task<IEnumerable<T2>> GetAllAsync() => await _service.GetAllAsync();
        public IEnumerable<T2> GetAllIgnoreFilters()
        {
            return _repository.GetAllIgnoreFilter<T1, T2>();
        }

        public T2 GetById(T1 id) => _service.GetById(id);
        public async Task<T2> GetByIdAsync(T1 id) => await _service.GetByIdAsync(id);
        public T2 Update(T1 id, T2 entity) => _service.Update(id, entity);
        public async Task<T2> UpdateAsync(T1 id, T2 entity) => await _service.UpdateAsync(id, entity);
    }
}