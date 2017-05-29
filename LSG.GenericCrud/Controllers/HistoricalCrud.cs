using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;

namespace LSG.GenericCrud.Controllers
{
    public class HistoricalCrud<T> : Crud<T>
        where T : class, IEntity, new()
    {
        private readonly Crud<HistoricalEvent> _historicalDal;

        public HistoricalCrud(IDbContext context) : base(context)
        {
            base.AutoCommit = false;
            _historicalDal = new Crud<HistoricalEvent>(context);
            _historicalDal.AutoCommit = false;
        }

        public override T Create(T entity)
        {
            var historicalEvent = new HistoricalEvent
            {
                Action = HistoricalActions.Create.ToString(),
                Changeset = new T().DetailedCompare(entity),
                EntityId = entity.Id,
                EntityName = entity.GetType().Name
            };

            base.Create(entity);
            _historicalDal.Create(historicalEvent);

            Context.SaveChanges();

            return entity;
        }

        public override void Update(Guid id, T entity)
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
            _historicalDal.Create(historicalEvent);

            // update original entity with modified fields
            foreach (var prop in originalEntity.GetType().GetProperties(
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.DeclaredOnly))
            {
                if (prop.Name != "Id")
                {
                    var oldValue = prop.GetValue(originalEntity, null);
                    var newValue = prop.GetValue(entity, null);
                    if (!oldValue.Equals(newValue))
                    {
                        var originalProperty = originalEntity.GetType().GetProperty(prop.Name);
                        var value = prop.GetValue(entity, null);
                        originalProperty.SetValue(originalEntity, value);
                    }
                }
            }

            Context.SaveChanges();
        }

        public override void Delete(Guid id)
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
            _historicalDal.Create(historicalEvent);

            base.Delete(id);

            Context.SaveChanges();
        }
    }
}
