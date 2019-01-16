using LSG.GenericCrud.Models;
using System;
using System.Collections.Generic;
using System.Text;
using LSG.GenericCrud.Repositories;
using System.Threading.Tasks;
using System.Linq;

namespace LSG.GenericCrud.Services
{
    public class ReadeableCrudService<T> : 
        IReadeableCrudService<T>
        where T : BaseEntity, IEntity, new()
    {
        private readonly ICrudRepository _repository;
        private readonly IUserInfoRepository _userInfoRepository;

        public ReadeableCrudService(ICrudRepository repository, IUserInfoRepository userInfoRepository)
        {
            _repository = repository;
            _userInfoRepository = userInfoRepository;
        }

        public async Task<object> GetAllAsync()
        {
            var entityStatuses = await _repository.GetAllAsync<EntityUserStatus>();
            var users = await _repository.GetAllAsync<User>();

            var entityName = typeof(T).FullName;

            var result =
                from a in _repository.GetAll<T>().AsQueryable()
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
                        NewStuffAvailable = !((a.ModifiedDate == null && au1.LastViewed >= a.CreatedDate) || (a.ModifiedDate != null && au1.LastViewed >= a.ModifiedDate))
                    }
                };
            return null;
        }

        public async Task<int> MarkAllAsRead()
        {
            var items = _repository.GetAll<T>();
            foreach (var item in items)
            {
                await _repository.CreateAsync(new EntityUserStatus() { Id = Guid.NewGuid(), EntityName = typeof(T).FullName, EntityId = item.Id, UserId = _userInfoRepository.GetUserInfo(), LastViewed = DateTime.Now });
            }
            return await _repository.SaveChangesAsync();
        }

        public async Task<int> MarkAllAsUnread()
        {
            var objects = _repository.GetAll<EntityUserStatus>().Where(_ => _.EntityName == typeof(T).FullName && _.UserId == _userInfoRepository.GetUserInfo()).ToList();
            objects.ForEach(async _ => await _repository.DeleteAsync<EntityUserStatus>(_.Id));
            return await _repository.SaveChangesAsync();
        }

        public async Task<int> MarkOneAsRead(Guid id)
        {
            await _repository.CreateAsync(new EntityUserStatus() { Id = Guid.NewGuid(), EntityName = typeof(T).FullName, EntityId = id, UserId = _userInfoRepository.GetUserInfo(), LastViewed = DateTime.Now });
            return await _repository.SaveChangesAsync();
        }

        public async Task<int> MarkOneAsUnread(Guid id)
        {
            var objects = _repository.GetAll<EntityUserStatus>().Where(_ => _.EntityName == typeof(T).FullName && _.UserId == _userInfoRepository.GetUserInfo() && _.EntityId == id).ToList();
            objects.ForEach(async _ => await _repository.DeleteAsync<EntityUserStatus>(_.Id));
            return await _repository.SaveChangesAsync();
        }
    }
}
