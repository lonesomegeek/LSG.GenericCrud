using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

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

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual T GetById(Guid id)
        {
            return _context.Set<T>().SingleOrDefault(_ => _.Id == id);
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(_ => _.Id == id);
        }

        public virtual T Create(T entity)
        {
            return _context.Set<T>().Add(entity).Entity;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            var result = await _context.Set<T>().AddAsync(entity);
            return result.Entity;
        }

        public T Update(Guid id, T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, T entity)
        {
            throw new NotImplementedException();
        }

        public virtual T Delete(Guid id)
        {
            return _context.Set<T>().Remove(GetById(id)).Entity;
        }

        public virtual async Task<T> DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            return _context.Set<T>().Remove(entity).Entity;
        }

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
