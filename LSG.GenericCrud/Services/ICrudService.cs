using System.Collections.Generic;

namespace LSG.GenericCrud.Services
{
    public interface ICrudService<T>
    {
        IList<T> GetAll();
    }
}