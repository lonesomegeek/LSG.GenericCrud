using LSG.GenericCrud.Models;
using System;
using System.Collections.Generic;
using System.Text;
using LSG.GenericCrud.Repositories;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Services
{
    public class ReadeableCrudService<T> : 
        IReadeableCrudService<T>
        where T : class, IEntity, new()
    {
        private readonly ICrudRepository _repository;

        public ReadeableCrudService(ICrudRepository repository)
        {
            _repository = repository;
        }

        public Task<object> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task MarkAsRead()
        {
            throw new NotImplementedException();
        }

        public Task MarkAsRead(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task MarkAsUnread()
        {
            throw new NotImplementedException();
        }

        public Task MarkAsUnread(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task MarkOneAsUnread(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
