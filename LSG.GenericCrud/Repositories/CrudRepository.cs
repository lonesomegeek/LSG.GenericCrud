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
        public CrudRepository() { }

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
        public virtual IQueryable<T> GetAll<T>() where T : class, IEntity, new() => GetAll<Guid, T>();

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T2> GetAll<T1, T2>() where T2 : class, IEntity<T1>, new() => _context.Set<T2>();

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IQueryable<T>> GetAllAsync<T>() where T : class, IEntity, new() => await Task.FromResult(GetAll<Guid, T>());

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IQueryable<T2>> GetAllAsync<T1, T2>() where T2 : class, IEntity<T1>, new() => await Task.FromResult(GetAll<T1, T2>());

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual T GetById<T>(Guid id) where T : class, IEntity, new() => GetById<Guid, T>(id);

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual T2 GetById<T1, T2>(T1 id) where T2 : class, IEntity<T1>, new() => GetByIdAsync<T1, T2>(id).GetAwaiter().GetResult();

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual async Task<T> GetByIdAsync<T>(Guid id) where T : class, IEntity, new() => await GetByIdAsync<Guid, T>(id);
        
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual async Task<T2> GetByIdAsync<T1, T2>(T1 id) where T2 : class, IEntity<T1>, new() => await _context.Set<T2>().FindAsync(id);
        
        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual T Create<T>(T entity) where T : class, IEntity, new() => CreateAsync(entity).GetAwaiter().GetResult();
        

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<T> CreateAsync<T>(T entity) where T : class, IEntity, new() => await CreateAsync<Guid, T>(entity);
        public virtual async Task<T2> CreateAsync<T1, T2>(T2 entity) where T2 : class, IEntity<T1>, new()
        {
            var result = await _context.Set<T2>().AddAsync(entity);
            return result.Entity;
        }
            
        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual T Update<T>(Guid id, T entity) where T : class, IEntity, new() => Update<Guid, T>(id, entity);

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual T2 Update<T1, T2>(T1 id, T2 entity) where T2 : class, IEntity<T1>, new() => UpdateAsync(id, entity).GetAwaiter().GetResult();

        public Task<T> UpdateAsync<T>(Guid id, T entity) where T : class, IEntity, new() => UpdateAsync<Guid, T>(id, entity);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual Task<T2> UpdateAsync<T1, T2>(T1 id, T2 entity) where T2 : class, IEntity<T1>, new() => throw new NotImplementedException();

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual T Delete<T>(Guid id) where T : class, IEntity, new() => Delete<Guid, T>(id);
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual T2 Delete<T1, T2>(T1 id) where T2 : class, IEntity<T1>, new() => DeleteAsync<T1, T2>(id).GetAwaiter().GetResult();

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual async Task<T> DeleteAsync<T>(Guid id) where T : class, IEntity, new() => await DeleteAsync<Guid, T>(id);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual async Task<T2> DeleteAsync<T1, T2>(T1 id) where T2 : class, IEntity<T1>, new()
        {
            var entity = await GetByIdAsync<T1, T2>(id);
            return _context.Set<T2>().Remove(entity).Entity;
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns>Number of elements saved.</returns>
        public virtual async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
