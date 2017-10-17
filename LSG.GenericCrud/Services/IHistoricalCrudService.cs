using System;
using System.Collections.Generic;
using System.Text;
using LSG.GenericCrud.Models;

namespace LSG.GenericCrud.Services
{
    public interface IHistoricalCrudService<T> : ICrudService<T>
    {
        T Restore(Guid id);
        IEnumerable<IEntity> GetHistory(Guid id);
    }
}
