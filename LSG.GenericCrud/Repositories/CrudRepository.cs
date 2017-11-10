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
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="LSG.GenericCrud.Repositories.ICrudRepository{T}" />
    public class CrudRepository : ICrudRepository
       
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly IDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudRepository{T}"/> class.
        /// </summary>
        public CrudRepository()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudRepository{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CrudRepository(IDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll<T>() where T : class, IEntity, new()
        {
            return _context.Set<T>();
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync<T>() where T : class, IEntity, new()
        {
            return await _context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual T GetById<T>(Guid id) where T : class, IEntity, new()
        {
            return _context.Set<T>().SingleOrDefault(_ => _.Id == id);
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual async Task<T> GetByIdAsync<T>(Guid id) where T : class, IEntity, new()
        {
            return await _context.Set<T>().SingleOrDefaultAsync(_ => _.Id == id);
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual T Create<T>(T entity) where T : class, IEntity, new()
        {
            return _context.Set<T>().Add(entity).Entity;
        }

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<T> CreateAsync<T>(T entity) where T : class, IEntity, new()
        {
            var result = await _context.Set<T>().AddAsync(entity);
            return result.Entity;
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T Update<T>(Guid id, T entity) where T : class, IEntity, new()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task UpdateAsync<T>(Guid id, T entity) where T : class, IEntity, new()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual T Delete<T>(Guid id) where T : class, IEntity, new()
        {
            return _context.Set<T>().Remove(GetById<T>(id)).Entity;
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual async Task<T> DeleteAsync<T>(Guid id) where T : class, IEntity, new()
        {
            var entity = await GetByIdAsync<T>(id);
            return _context.Set<T>().Remove(entity).Entity;
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
