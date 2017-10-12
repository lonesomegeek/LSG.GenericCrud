using System;
using System.Collections.Generic;
using LSG.GenericCrud.Models;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.Services
{
    public interface ICrudService<T>
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        T Create(IEntity entity);
        void Update(Guid id, IEntity entity);
        void Delete(Guid id);
    }
}