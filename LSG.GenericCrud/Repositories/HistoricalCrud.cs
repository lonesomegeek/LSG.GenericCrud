using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LSG.GenericCrud.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="LSG.GenericCrud.Repositories.Crud{T}" />
    public class HistoricalCrud<T> : Crud<T>
        where T : class, IEntity, new()
    {
        /// <summary>
        /// The historical dal
        /// </summary>
        private readonly Crud<HistoricalEvent> _dal;

        /// <summary>
        /// Default parameterless consutrctor
        /// </summary>
        public HistoricalCrud() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoricalCrud{T}" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public HistoricalCrud(IDbContext context) : base(context)
        {
            base.AutoCommit = false;
            _dal = new Crud<HistoricalEvent>(context);
            _dal.AutoCommit = false;
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public override T Create(T entity)
        {
            // check for uninitialized id
            if (entity.Id == Guid.Empty) entity.Id = Guid.NewGuid();

            var historicalEvent = new HistoricalEvent
            {
                Action = HistoricalActions.Create.ToString(),
                Changeset = new T().DetailedCompare(entity),
                EntityId = entity.Id,
                EntityName = entity.GetType().Name
            };

            base.Create(entity);
            _dal.Create(historicalEvent);

            Context.SaveChanges();

            return entity;
        }

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public override async Task<T> CreateAsync(T entity)
        {
            // check for uninitialized id
            if (entity.Id == Guid.Empty) entity.Id = Guid.NewGuid();

            var historicalEvent = new HistoricalEvent
            {
                Action = HistoricalActions.Create.ToString(),
                Changeset = new T().DetailedCompare(entity),
                EntityId = entity.Id,
                EntityName = entity.GetType().Name
            };

            await base.CreateAsync(entity);
            await _dal.CreateAsync(historicalEvent);

            await Context.SaveChangesAsync();

            return entity;
        }


        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public override T Update(Guid id, T entity)
        {
            var originalEntity = base.GetById(id);

            // create historical change
            var historicalEvent = new HistoricalEvent
            {
                Action = HistoricalActions.Update.ToString(),
                Changeset = originalEntity.DetailedCompare(entity),
                EntityId = originalEntity.Id,
                EntityName = entity.GetType().Name
            };
            _dal.Create(historicalEvent);

            // update original entity with modified fields
            foreach (var prop in originalEntity.GetType().GetProperties(
                        BindingFlags.Public |
                        BindingFlags.Instance |
                        BindingFlags.DeclaredOnly))
            {
                if (prop.Name != "Id")
                {
                    var oldValue = prop.GetValue(originalEntity, null);
                    var newValue = prop.GetValue(entity, null);
                    if (oldValue == null || !oldValue.Equals(newValue))
                    {
                        var originalProperty = originalEntity.GetType().GetProperty(prop.Name);
                        var value = prop.GetValue(entity, null);
                        originalProperty.SetValue(originalEntity, value);
                    }
                }
            }

            Context.SaveChanges();

            return originalEntity;
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public override async Task UpdateAsync(Guid id, T entity)
        {
            var originalEntity = await base.GetByIdAsync(id);

            // create historical change
            var historicalEvent = new HistoricalEvent
            {
                Action = HistoricalActions.Update.ToString(),
                Changeset = originalEntity.DetailedCompare(entity),
                EntityId = originalEntity.Id,
                EntityName = entity.GetType().Name
            };
            await _dal.CreateAsync(historicalEvent);

            // update original entity with modified fields
            foreach (var prop in originalEntity.GetType().GetProperties(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.DeclaredOnly))
            {
                if (prop.Name != "Id")
                {
                    var oldValue = prop.GetValue(originalEntity, null);
                    var newValue = prop.GetValue(entity, null);
                    if (oldValue == null || !oldValue.Equals(newValue))
                    {
                        var originalProperty = originalEntity.GetType().GetProperty(prop.Name);
                        var value = prop.GetValue(entity, null);
                        originalProperty.SetValue(originalEntity, value);
                    }
                }
            }

            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public override T Delete(Guid id)
        {
            var entity = base.GetById(id);

            // store all object in historical event
            var historicalEvent = new HistoricalEvent
            {
                Action = HistoricalActions.Delete.ToString(),
                Changeset = new T().DetailedCompare(entity),
                EntityId = entity.Id,
                EntityName = entity.GetType().Name
            };
            _dal.Create(historicalEvent);

            base.Delete(id);

            Context.SaveChanges();

            return null;
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public override async Task<T> DeleteAsync(Guid id)
        {
            var entity = await base.GetByIdAsync(id);

            // store all object in historical event
            var historicalEvent = new HistoricalEvent
            {
                Action = HistoricalActions.Delete.ToString(),
                Changeset = new T().DetailedCompare(entity),
                EntityId = entity.Id,
                EntityName = entity.GetType().Name
            };
            await _dal.CreateAsync(historicalEvent);

            await base.DeleteAsync(id);

            await Context.SaveChangesAsync();

            return null;
        }

        /// <summary>
        /// Restores the specified entity identifier.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        /// <returns></returns>
        /// <exception cref="LSG.GenericCrud.Exceptions.EntityNotFoundException"></exception>
        public virtual T Restore(Guid entityId)
        {
            var originalEntity = _dal
                .GetAll()
                .SingleOrDefault(_ =>
                    _.EntityId == entityId &&
                    _.Action == HistoricalActions.Delete.ToString() /*&& _.EntityName == entityName*/);
            if (originalEntity == null) throw new EntityNotFoundException();

            var json = originalEntity.Changeset;
            var obj = JsonConvert.DeserializeObject<T>(json);
            var createdObject = Create(obj);
            return createdObject;
        }

        /// <summary>
        /// Restores the asynchronous.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        /// <returns></returns>
        /// <exception cref="LSG.GenericCrud.Exceptions.EntityNotFoundException"></exception>
        public virtual async Task<object> RestoreAsync(Guid entityId)
        {
            var originalEntity = _dal
                .GetAll()
                .SingleOrDefault(_ =>
                    _.EntityId == entityId &&
                    _.Action == HistoricalActions.Delete.ToString() /*&& _.EntityName == entityName*/);
            if (originalEntity == null) throw new EntityNotFoundException();

            var json = originalEntity.Changeset;
            var obj = JsonConvert.DeserializeObject<T>(json);
            var createdObject = await CreateAsync(obj);
            return createdObject;
        }

        /// <summary>
        /// Gets the history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual IEnumerable<IEntity> GetHistory(Guid id)
        {
            return _dal
                .GetAll()
                .Where(_ => _.EntityId == id);
        }

        /// <summary>
        /// Gets the history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<IEntity>> GetHistoryAsync(Guid id)
        {
            return _dal
                .GetAll()
                .Where(_ => _.EntityId == id);
        }


    }
}
