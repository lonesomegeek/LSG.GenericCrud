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
        }

        public IEnumerable<T> GetAll() => _repository.GetAll();

        public T GetById(Guid id)
        {
            var entity = _repository.GetById(id);
            if (entity == null) throw new EntityNotFoundException();
            return entity;
        }

        public T Create(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid id, IEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}