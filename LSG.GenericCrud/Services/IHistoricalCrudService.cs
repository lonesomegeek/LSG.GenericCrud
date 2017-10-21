using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;

namespace LSG.GenericCrud.Services
{
    public interface IHistoricalCrudService<T> : ICrudService<T>
    {
        T Restore(Guid id);
        IEnumerable<IEntity> GetHistory(Guid id);
        Task<T> RestoreAsync(Guid id);
        Task<IEnumerable<IEntity>> GetHistoryAsync(Guid id);
    }
}
