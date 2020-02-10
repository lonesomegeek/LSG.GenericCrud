﻿using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Samples.Services
{
    public class CustomInheritedCrudService<T1, T2> : CrudServiceBase<T1, T2> where T2 : class, IEntity<T1>, new()
    {
        public CustomInheritedCrudService(
            ICrudRepository repository,
            ILogger<CustomInheritedCrudService<T1, T2>> logger) : base(repository) 
        {
            logger.LogInformation($"In custom service layer for {typeof(T2).Name}");
        }

        public override Task<T2> CopyAsync(T1 id) => base.CopyAsync(id);
        public override Task<T2> CreateAsync(T2 entity) => base.CreateAsync(entity);
        public override Task<T2> DeleteAsync(T1 id) => base.DeleteAsync(id);
        public override Task<IEnumerable<T2>> GetAllAsync() => base.GetAllAsync();
        public override Task<T2> GetByIdAsync(T1 id) => base.GetByIdAsync(id);
        public override Task<T2> UpdateAsync(T1 id, T2 entity) => base.UpdateAsync(id, entity);        
    }

    public class CustomImplementedCrudService<T1, T2> : ICrudService<T1, T2> where T2 : class, IEntity<T1>, new()
    {
        private readonly CrudServiceBase<T1, T2> _service;

        public CustomImplementedCrudService(CrudServiceBase<T1, T2> service,
            ILogger<CustomImplementedCrudService<T1, T2>> logger)
        {

            _service = service;
            logger.LogInformation($"In custom service layer for {typeof(T2).Name}");
        }

        public bool AutoCommit { get; set; }

        public async Task<T2> CopyAsync(T1 id) => await _service.CopyAsync(id);
        public async Task<T2> CreateAsync(T2 entity) => await _service.CreateAsync(entity);
        public async Task<T2> DeleteAsync(T1 id) => await _service.DeleteAsync(id);
        public async Task<IEnumerable<T2>> GetAllAsync() => await _service.GetAllAsync();
        public async Task<T2> GetByIdAsync(T1 id) => await _service.GetByIdAsync(id);
        public async Task<T2> UpdateAsync(T1 id, T2 entity) => await _service.UpdateAsync(id, entity);
    }
}

// todo: custom tasks before or after the base code
// todo: complete custom layer