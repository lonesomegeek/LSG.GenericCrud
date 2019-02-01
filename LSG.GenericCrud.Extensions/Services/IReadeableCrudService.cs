using LSG.GenericCrud.Models;
using System;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Extensions.Services
{
    public interface IReadeableCrudService<T> where T : class, IEntity, new()
    {
        Task<object> GetAllAsync();
        Task<int> MarkAllAsRead();
        Task<int> MarkOneAsRead(Guid id);
        Task<int> MarkAllAsUnread();
        Task<int> MarkOneAsUnread(Guid id);
    }
}