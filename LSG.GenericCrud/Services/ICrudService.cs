using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.Services
{
    public interface ICrudService<T>
    {
        bool AutoCommit { get; set; }
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        T Create(T entity);
        T Update(Guid id, T entity);
        T Delete(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(Guid id, T entity);
        Task<T> DeleteAsync(Guid id);
    }
}