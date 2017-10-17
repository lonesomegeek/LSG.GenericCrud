using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Newtonsoft.Json;

namespace LSG.GenericCrud.Services
{
    public class HistoricalCrudService<T> : CrudService<T>, IHistoricalCrudService<T> where T : IEntity
    {
        private readonly ICrudRepository<HistoricalEvent> _eventRepository;

        public HistoricalCrudService(ICrudRepository<T> repository, ICrudRepository<HistoricalEvent> eventRepository) : base(repository)
        {
            _eventRepository = eventRepository;
            AutoCommit = false;
        }

        public override T Create(T entity)
        {
            return base.Create(entity);
        }

        public override T Update(Guid id, T entity)
        {
            return base.Update(id, entity);
        }

        public override T Delete(Guid id)
        {
            return base.Delete(id);
        }

        public T Restore(Guid id)
        {
            var entity = _eventRepository
                .GetAll()
                .SingleOrDefault(_ =>
                    _.EntityId == id &&
                    _.Action == HistoricalActions.Delete.ToString());
            //if (originalEntity == null) throw new EntityNotFoundException();
            var json = entity.Changeset;
            var obj = JsonConvert.DeserializeObject<T>(json);
            var createdObject = Create(obj);
            return createdObject;
        }

        public IEnumerable<IEntity> GetHistory(Guid id)
        {
            throw new NotImplementedException();
        }


    }
}
