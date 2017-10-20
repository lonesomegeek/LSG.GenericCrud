using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Newtonsoft.Json;

namespace LSG.GenericCrud.Services
{
    public class HistoricalCrudService<T> : CrudService<T>, IHistoricalCrudService<T> where T : IEntity, new()
    {
        private readonly ICrudRepository<HistoricalEvent> _eventRepository;
        private readonly ICrudRepository<T> _entityRepository;

        public HistoricalCrudService(ICrudRepository<T> repository, ICrudRepository<HistoricalEvent> eventRepository) : base(repository)
        {
            _entityRepository = repository;
            _eventRepository = eventRepository;
            AutoCommit = false;
        }

        public override T Create(T entity)
        {
            var createdEntity = base.Create(entity);

            var historicalEvent = new HistoricalEvent
            {
                Action = HistoricalActions.Create.ToString(),
                Changeset = new T().DetailedCompare(entity),
                EntityId = entity.Id,
                EntityName = entity.GetType().Name
            };

            _eventRepository.Create(historicalEvent);
            _entityRepository.SaveChanges();
            // TODO: Do I need to call the other repo for both repositories, or do I need a UoW
            return createdEntity;
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
            if (entity == null) throw new EntityNotFoundException();
            var json = entity.Changeset;
            var obj = JsonConvert.DeserializeObject<T>(json);
            var createdObject = Create(obj);
            return createdObject;
        }

        public IEnumerable<IEntity> GetHistory(Guid id)
        {
            var events = _eventRepository
                .GetAll()
                .Where(_ => _.EntityId == id).ToList();
            if (!events.Any()) throw new EntityNotFoundException();
            return events;
        }


    }
}
