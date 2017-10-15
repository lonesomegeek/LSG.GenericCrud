using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GenericCrud.Services
{
    public interface IHistoricalCrudService<T>
    {
        T Restore(Guid id);
    }
}
