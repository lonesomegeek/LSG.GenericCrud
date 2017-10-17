using System;
using System.Collections.Generic;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;

namespace LSG.GenericCrud.Services
{
    public class CrudService<T> : ICrudService<T>
    {
        private readonly ICrudRepository<T> _repository;

        public CrudService(ICrudRepository<T> repository)
        {
            _repository = repository;
            AutoCommit = true;
        }

        public bool AutoCommit { get; set; }

        public IEnumerable<T> GetAll() => _repository.GetAll();

        public T GetById(Guid id)
        {
            var entity = _repository.GetById(id);
            if (entity == null) throw new EntityNotFoundException();
            return entity;
        }

        public virtual T Create(T entity)
        {
            var createdEntity = _repository.Create(entity);
            if (AutoCommit) _repository.SaveChanges();
            return createdEntity;
        }

        public virtual T Update(Guid id, T entity)
        {
            var originalEntity = GetById(id);
            foreach (var prop in entity.GetType().GetProperties())
            {
                if (prop.Name != "Id")
                {
                    var originalProperty = originalEntity.GetType().GetProperty(prop.Name);
                    var value = prop.GetValue(entity, null);
                    if (value != null) originalProperty.SetValue(originalEntity, value);
                }
            }
            if (AutoCommit) _repository.SaveChanges();
            return originalEntity;
        }

        public virtual T Delete(Guid id)
        {
            var entity = GetById(id);
            _repository.Delete(id);
            if (AutoCommit) _repository.SaveChanges();
            return entity;
        }
    }
}