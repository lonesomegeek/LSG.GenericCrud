using System;
using System.Collections.Generic;
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
    }
}