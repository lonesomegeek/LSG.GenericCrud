using LSG.GenericCrud.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Extensions.Services
{
    public interface IReadeableCrudService<T> where T : class, IEntity, new()
    {
        Task<IEnumerable<ReadeableStatus<T>>> GetAllAsync();
        Task<ReadeableStatus<T>> GetByIdAsync(Guid id);
        Task<int> MarkAllAsRead();
        Task<int> MarkOneAsRead(Guid id);
        Task<int> MarkAllAsUnread();
        Task<int> MarkOneAsUnread(Guid id);
    }
}