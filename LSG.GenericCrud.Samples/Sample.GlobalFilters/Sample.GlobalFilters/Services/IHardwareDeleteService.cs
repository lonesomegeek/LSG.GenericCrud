using LSG.GenericCrud.Models;
using System.Threading.Tasks;

namespace Sample.GlobalFilters.Services
{
    public interface IHardwareDeleteService<T1, T2> where T2 : class, IEntity<T1>, new()
    {
        Task<T2> DeleteHardAsync(T1 id);
    }
}