using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace LSG.GenericCrud.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="LSG.GenericCrud.Repositories.ICrud{T}" />
    public class Crud<T> : ICrud<T>
        where T : class, IEntity, new()
    {
        /// <summary>
        /// The context
        /// </summary>
        protected IDbContext Context;

        /// <summary>
        /// Gets or sets a value indicating whether [automatic commit].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [automatic commit]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoCommit { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Crud{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Crud(IDbContext context)
        {
            Context = context;
            AutoCommit = true;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll() => Context.Set<T>().AsEnumerable();

        /// <summary>
        /// Get all async.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync() => await Context.Set<T>().ToListAsync();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="LSG.GenericCrud.Exceptions.EntityNotFoundException"></exception>
        public T GetById(Guid id)
        {
            var entity = Context.Set<T>().SingleOrDefault(_ => _.Id == id);
            if (entity == null) throw new EntityNotFoundException();
            else return entity;
        }

        /// <summary>
        /// Gets the by identifier async.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="LSG.GenericCrud.Exceptions.EntityNotFoundException"></exception>
        public async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await Context.Set<T>().SingleOrDefaultAsync(_ => _.Id == id);
            if (entity == null) throw new EntityNotFoundException();
            else return entity;
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual T Create(T entity)
        {
            var returnEntity = Context.Set<T>().Add(entity).Entity;
            if (AutoCommit) Context.SaveChanges();
            return returnEntity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var returnEntity = await Context.Set<T>().AddAsync(entity);
            if (AutoCommit) await Context.SaveChangesAsync();
            return returnEntity.Entity;
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        public virtual void Update(Guid id, T entity)
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
            if (AutoCommit) Context.SaveChanges();
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void Delete(Guid id)
        {
            Context.Set<T>().Remove(GetById(id));
            if (AutoCommit) Context.SaveChanges();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICrud<T>
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T GetById(Guid id);
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        T Create(T entity);
        Task<T> CreateAsync(T entity);

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        void Update(Guid id, T entity);
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(Guid id);


    }
}
