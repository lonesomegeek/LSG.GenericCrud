using LSG.GenericCrud.Extensions.DataFillers;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Extensions.Services
{
    public class ReadeableCrudService<T> :
        IReadeableCrudService<T>
        where T : BaseEntity, IEntity, new()
    {
        private readonly ICrudRepository _repository;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IReadeableCrudOptions _options;

        public ReadeableCrudService(
            ICrudRepository repository,
            IUserInfoRepository userInfoRepository,
            IReadeableCrudOptions options)
        {
            _repository = repository;
            _userInfoRepository = userInfoRepository;
            _options = options;
        }

        public async Task<ReadeableStatus<T>> GetByIdAsync(Guid id)
        {
            var entityStatuses = await _repository.GetAllAsync<EntityUserStatus>();
            var users = await _repository.GetAllAsync<User>();

            var entityName = typeof(T).FullName;

            var entity = await _repository.GetByIdAsync<T>(id);

            var result =
                from au1 in entityStatuses
                where au1.EntityId == id && au1.EntityName == entityName

                // left join users
                join u in users on au1.UserId equals u.Id.ToString() into u_g
                from u in u_g.DefaultIfEmpty()

                    // left join users & accounts on accountsusers
                where au1 == null || au1.UserId == _userInfoRepository.GetUserInfo()
                select new ReadeableStatus<T>
                {
                    Data = entity,
                    Metadata = new ReadeableStatusMetadata
                    {
                        NewStuffAvailable = IsNewStuffAvailable(entity, au1),
                        LastViewed = au1.LastViewed
                    }
                };
            return null;
        }

        public async Task<IEnumerable<ReadeableStatus<T>>> GetAllAsync()
        {
            var entityStatuses = await _repository.GetAllAsync<EntityUserStatus>();
            var users = await _repository.GetAllAsync<User>();

            var entityName = typeof(T).FullName;

            var result =
                from a in _repository
                    .GetAll<T>()
                    .AsQueryable()
                    // left join account users
                join au1 in entityStatuses on new { EntityId = a.Id, EntityName = entityName } equals new { au1.EntityId, au1.EntityName } into au1_g
                from au1 in au1_g.DefaultIfEmpty()

                    // left join users
                join u in users on au1.UserId equals u.Id.ToString() into u_g
                from u in u_g.DefaultIfEmpty()

                    // left join users & accounts on accountsusers
                where au1 == null || au1.UserId == _userInfoRepository.GetUserInfo()
                select new ReadeableStatus<T>
                {
                    Data = a,
                    Metadata = new ReadeableStatusMetadata
                    {
                        NewStuffAvailable = IsNewStuffAvailable(a, au1),
                        LastViewed = au1.LastViewed
                    }
                };
            return result;
        }

        private bool IsNewStuffAvailable(T entity, EntityUserStatus status)
        {
            var createdByMe = entity.ModifiedBy == null && entity.CreatedBy == _userInfoRepository.GetUserInfo();
            var modifiedByMe = entity.ModifiedBy != null && entity.ModifiedBy == _userInfoRepository.GetUserInfo();
            var createdOrModifiedByMe = createdByMe || modifiedByMe;

            var createdBySomeone = entity.ModifiedBy == null && entity.CreatedBy != _userInfoRepository.GetUserInfo();
            var modifiedBySomeone = entity.ModifiedBy != null && entity.ModifiedBy != _userInfoRepository.GetUserInfo();
            var createdOrModifiedBySomeone = createdBySomeone || modifiedBySomeone;

            var viewedLately = entity.ModifiedDate == null && status?.LastViewed > entity.CreatedDate || entity.ModifiedDate != null && status?.LastViewed > entity.ModifiedDate;

            if (!viewedLately)
            {
                if (_options.ShowMyNewStuff && createdOrModifiedByMe) return true;
                else if (_options.ShowMyNewStuff && createdOrModifiedBySomeone) return true;
                else if (!_options.ShowMyNewStuff && createdOrModifiedBySomeone) return true;
                else return false;
            }
            else return false;
        }

        public async Task<int> MarkAllAsRead()
        {
            // checking for existing status
            var items = _repository
                .GetAll<T>()
                .Select(_ => _.Id)
                .ToList();
            var statuses = _repository
                .GetAll<EntityUserStatus>()
                .Where(_ => _.EntityName == typeof(T).FullName && _.UserId == _userInfoRepository.GetUserInfo());

            foreach (var id in items)
            {
                var status = statuses
                    .SingleOrDefault(_ => _.EntityName == typeof(T).FullName && _.UserId == _userInfoRepository.GetUserInfo() && _.EntityId == id);

                if (status == null)
                {
                    status = new EntityUserStatus() { Id = Guid.NewGuid(), EntityName = typeof(T).FullName, EntityId = id, UserId = _userInfoRepository.GetUserInfo(), LastViewed = DateTime.Now };
                    await _repository.CreateAsync(status);
                }
                else
                {
                    status.LastViewed = DateTime.Now;
                }
            }

            return await _repository.SaveChangesAsync();
        }

        public async Task<int> MarkAllAsUnread()
        {
            _repository
                .GetAll<EntityUserStatus>()
                .Where(_ => _.EntityName == typeof(T).FullName && _.UserId == _userInfoRepository.GetUserInfo())
                .ToList()
                .ForEach(_ => _.LastViewed = null);

            return await _repository.SaveChangesAsync();
        }

        public async Task<int> MarkOneAsRead(Guid id)
        {
            // checking for existing status
            var status = _repository
                .GetAll<EntityUserStatus>()
                .SingleOrDefault(_ => _.EntityName == typeof(T).FullName && _.UserId == _userInfoRepository.GetUserInfo() && _.EntityId == id);

            if (status == null)
            {
                status = new EntityUserStatus() { Id = Guid.NewGuid(), EntityName = typeof(T).FullName, EntityId = id, UserId = _userInfoRepository.GetUserInfo(), LastViewed = DateTime.Now };
                await _repository.CreateAsync(status);
            } else
            {
                status.LastViewed = DateTime.Now;
            }
            return await _repository.SaveChangesAsync();
        }

        public async Task<int> MarkOneAsUnread(Guid id)
        {
            _repository
                .GetAll<EntityUserStatus>()
                .SingleOrDefault(_ => _.EntityName == typeof(T).FullName && _.UserId == _userInfoRepository.GetUserInfo() && _.EntityId == id)
                .LastViewed = null;
            return await _repository.SaveChangesAsync();
        }
    }

    public class ReadeableStatus<T>
    {
        public T Data { get; set; }
        public ReadeableStatusMetadata Metadata { get; set; }
    }

    public class ReadeableStatusMetadata
    {
        public bool NewStuffAvailable { get; internal set; }
        public DateTime? LastViewed { get; internal set; }
    }
}
