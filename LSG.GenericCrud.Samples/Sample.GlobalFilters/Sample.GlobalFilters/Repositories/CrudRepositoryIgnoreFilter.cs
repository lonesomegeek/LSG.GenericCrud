using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Sample.GlobalFilters.Repositories
{
    public class CrudRepositoryIgnoreFilter : ICrudRepository
    {
        private readonly CrudRepository _repository;
        private readonly IDbContext _context;

        public CrudRepositoryIgnoreFilter(IDbContext context, CrudRepository repository)
        {
            _repository = repository;
            _context = context;
        }

        public IEnumerable<T2> GetAllIgnoreFilter<T1, T2>() where T2 : class, IEntity<T1>, new()
        {
            return _context.Set<T2>().IgnoreQueryFilters().ToList();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        T ICrudRepository.Create<T>(T entity)
        {
            throw new NotImplementedException();
        }

        Task<T> ICrudRepository.CreateAsync<T>(T entity)
        {
            throw new NotImplementedException();
        }

        Task<T2> ICrudRepository.CreateAsync<T1, T2>(T2 entity)
        {
            throw new NotImplementedException();
        }

        T ICrudRepository.Delete<T>(Guid id)
        {
            throw new NotImplementedException();
        }

        T2 ICrudRepository.Delete<T1, T2>(T1 id)
        {
            throw new NotImplementedException();
        }

        Task<T> ICrudRepository.DeleteAsync<T>(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<T2> ICrudRepository.DeleteAsync<T1, T2>(T1 id)
        {
            throw new NotImplementedException();
        }

        IQueryable<T> ICrudRepository.GetAll<T>()
        {
            throw new NotImplementedException();
        }

        IQueryable<T2> ICrudRepository.GetAll<T1, T2>()
        {
            throw new NotImplementedException();
        }

        Task<IQueryable<T>> ICrudRepository.GetAllAsync<T>()
        {
            throw new NotImplementedException();
        }

        Task<IQueryable<T2>> ICrudRepository.GetAllAsync<T1, T2>()
        {
            throw new NotImplementedException();
        }

        T ICrudRepository.GetById<T>(Guid id)
        {
            throw new NotImplementedException();
        }

        T2 ICrudRepository.GetById<T1, T2>(T1 id)
        {
            throw new NotImplementedException();
        }

        Task<T> ICrudRepository.GetByIdAsync<T>(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<T2> ICrudRepository.GetByIdAsync<T1, T2>(T1 id)
        {
            throw new NotImplementedException();
        }

        T ICrudRepository.Update<T>(Guid id, T entity)
        {
            throw new NotImplementedException();
        }

        T2 ICrudRepository.Update<T1, T2>(T1 id, T2 entity)
        {
            throw new NotImplementedException();
        }

        Task<T> ICrudRepository.UpdateAsync<T>(Guid id, T entity)
        {
            throw new NotImplementedException();
        }

        Task<T2> ICrudRepository.UpdateAsync<T1, T2>(T1 id, T2 entity)
        {
            throw new NotImplementedException();
        }
    }
}