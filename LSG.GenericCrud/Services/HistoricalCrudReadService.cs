using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LSG.GenericCrud.Helpers;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;

namespace LSG.GenericCrud.Services
{
    public class HistoricalCrudReadService<T1, T2> : IHistoricalCrudReadService<T1, T2> where T2 : class, IEntity<T1>, new()
    {
        private readonly ICrudRepository _repository;
        private readonly IUserInfoRepository _userInfoRepository;

        public HistoricalCrudReadService(
            ICrudRepository repository,
            IUserInfoRepository userInfoRepository)
        {
            _repository = repository;
            _userInfoRepository = userInfoRepository;
        }
        public List<Change> ExtractChanges<T>(T source, T destination)
        {
            if (destination == null) throw new ArgumentNullException(nameof(destination));
            var changes = new List<Change>();

            destination
                .GetType()
                .GetProperties()
                .Where(_ => _.DeclaringType == destination.GetType() && !Attribute.IsDefined(_, typeof(IgnoreInChangesetAttribute)))
                .ToList()
                .ForEach(_ => changes.Add(new Change()
                {
                    FieldName = _.Name,
                    FromValue = source.GetType().GetProperty(_.Name)?.GetValue(source),
                    ToValue = destination.GetType().GetProperty(_.Name)?.GetValue(destination)
                }));

            return changes;
        }

        public DateTime? GetLastTimeViewed<T2>(T1 id)
        {
            var lastView = _repository
                .GetAll<HistoricalEvent>()
                .SingleOrDefault(_ =>
                    _.EntityId == id.ToString() &&
                    _.EntityName == typeof(T2).FullName &&
                    _.Action == HistoricalActions.Read.ToString() &&
                    _.CreatedBy == _userInfoRepository.GetUserInfo());
            return lastView?.CreatedDate ?? DateTime.MinValue;
        }
    }

    public interface IHistoricalCrudReadService<T1, T2> where T2 : class, IEntity<T1>, new()
    {
        List<Change> ExtractChanges<T>(T source, T destination);
        DateTime? GetLastTimeViewed<T2>(T1 id);
    }
}
