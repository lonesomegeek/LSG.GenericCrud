using System;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;

namespace LSG.GenericCrud.Services
{
    public interface IReadeableCrudService<T> where T : class, IEntity, new()
    {
        Task<object> GetAllAsync();
        Task MarkAsRead();
        Task MarkAsRead(Guid id);
        Task MarkAsUnread();
        Task MarkAsUnread(Guid id);
        Task MarkOneAsUnread(Guid id);
    }
}