using LSG.GenericCrud.Extensions.DataFillers;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using System;
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

        public async Task<object> GetAllAsync()
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
                select new
                {
                    Data = a,
                    Metadata = new
                    {
                        NewStuffAvailable =
                        IsNewStuffAvailable(a, au1)
                        //(a.ModifiedBy == null && a.CreatedBy != _userInfoRepository.GetUserInfo()) &&
                        //(a.ModifiedBy != _userInfoRepository.GetUserInfo()) &&
                        //!((a.ModifiedDate == null && au1.LastViewed >= a.CreatedDate) || (a.ModifiedDate != null && au1.LastViewed >= a.ModifiedDate))
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
            //// TODO: lastviewed a NULL
            //var objects = _repository.GetAll<EntityUserStatus>().Where(_ => _.EntityName == typeof(T).FullName && _.UserId == _userInfoRepository.GetUserInfo()).ToList();
            //objects.ForEach(async _ => await _repository.DeleteAsync<EntityUserStatus>(_.Id));
            //return await _repository.SaveChangesAsync();

            _repository
                .GetAll<EntityUserStatus>()
                .Where(_ => _.EntityName == typeof(T).FullName && _.UserId == _userInfoRepository.GetUserInfo())
                .ToList()
                .ForEach(_ => _.LastViewed = null);

            return await _repository.SaveChangesAsync();
        }

        public async Task<int> MarkOneAsRead(Guid id)
        {
            //// TODO: Update lastviewed
            //var status = new EntityUserStatus() { Id = Guid.NewGuid(), EntityName = typeof(T).FullName, EntityId = id, UserId = _userInfoRepository.GetUserInfo(), LastViewed = DateTime.Now };
            //var statusToDelete = _repository.GetAll<EntityUserStatus>().Where(_ => _.EntityId == status.EntityId && _.EntityName == status.EntityName && _.UserId == status.UserId).ToList();
            //statusToDelete.ForEach(async _ => await _repository.DeleteAsync<EntityUserStatus>(_.Id));
            //await _repository.CreateAsync(status);
            //return await _repository.SaveChangesAsync();

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
}
