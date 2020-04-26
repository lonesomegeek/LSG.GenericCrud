using System;
using System.Collections.Generic;
using LSG.GenericCrud.Models;

namespace LSG.GenericCrud.Services
{
    public interface IHistoricalCrudReadService<T1, T2> where T2 : class, IEntity<T1>, new()
    {
        List<Change> ExtractChanges<T>(T source, T destination);
        DateTime? GetLastTimeViewed<T2>(T1 id);
    }
}
