using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LSG.GenericCrud.Models;

namespace LSG.GenericCrud.Repositories
{
    public class Crud<T> : ICrud<T>
        where T : class, IEntity, new()
    {
        protected IDbContext Context;

        public bool AutoCommit { get; set; }

        public Crud(IDbContext context)
        {
            Context = context;
            AutoCommit = true;
        }

        public IEnumerable<T> GetAll() => Context.Set<T>().AsEnumerable();

        public T GetById(Guid id) => Context.Set<T>().SingleOrDefault(_ => _.Id == id);

        public virtual T Create(T entity)
        {
            var returnEntity = Context.Set<T>().Add(entity).Entity;
            if (AutoCommit) Context.SaveChanges();
            return returnEntity;
        }

        public virtual void Update(Guid id, T entity)
        {
            var originalEntity = GetById(id);
            foreach (var prop in entity.GetType().GetProperties())
            {
                if (prop.Name != "Id")
                {
                    var originalProperty = originalEntity.GetType().GetProperty(prop.Name);
                    var value = prop.GetValue(entity, null);
                    originalProperty.SetValue(originalEntity, value);
                }
            }
            if (AutoCommit) Context.SaveChanges();
        }

        public virtual void Delete(Guid id)
        {
            Context.Set<T>().Remove(GetById(id));
            if (AutoCommit) Context.SaveChanges();
        }
    }

    public interface ICrud<T>
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        T Create(T entity);
        void Update(Guid id, T entity);
        void Delete(Guid id);
    }
}
