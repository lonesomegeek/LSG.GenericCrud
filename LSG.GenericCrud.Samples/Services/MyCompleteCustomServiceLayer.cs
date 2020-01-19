using LSG.GenericCrud.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Samples.Services
{
    public class MyCompleteCustomServiceLayer<T1, T2> : ICrudService<T1, T2>
    {
        public bool AutoCommit { get; set; }

        public Task<T2> CopyAsync(T1 id)
        {
            throw new NotImplementedException();
        }

        public Task<T2> CreateAsync(T2 entity)
        {
            throw new NotImplementedException();
        }

        public Task<T2> DeleteAsync(T1 id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T2>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T2> GetByIdAsync(T1 id)
        {
            throw new NotImplementedException();
        }

        public Task<T2> UpdateAsync(T1 id, T2 entity)
        {
            throw new NotImplementedException();
        }
    }
}
