using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;

namespace LSG.GenericCrud.Repositories
{
    public class CrudRepository<T> : ICrudRepository<T>
        where T : class, IEntity, new()
    {
        private readonly IDbContext _context;

        public CrudRepository()
        {
            
        }

        public CrudRepository(IDbContext context)
        {
            _context = context;
        }
        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public T GetById(Guid id)
        {
            return _context.Set<T>().SingleOrDefault(_ => _.Id == id);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public T Create(T entity)
        {
            return _context.Set<T>().Add(entity).Entity;
        }

        public Task<T> CreateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public T Update(Guid id, T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, T entity)
        {
            throw new NotImplementedException();
        }

        public T Delete(Guid id)
        {
            return _context.Set<T>().Remove(GetById(id)).Entity;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
