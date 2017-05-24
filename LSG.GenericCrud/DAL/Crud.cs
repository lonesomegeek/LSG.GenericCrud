using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace LSG.GenericCrud.DAL
{
    public class Crud<T> : ICrud<T>
        where T : class, IEntity, new()
    {
        private readonly IDbContext _context;

        public bool AutoCommit { get; private set; }

        public Crud(IDbContext context)
        {
            _context = context;
            AutoCommit = true;
        }

        public IEnumerable<T> GetAll() => _context.Set<T>().AsEnumerable();

        public T GetById(Guid id) => _context.Set<T>().SingleOrDefault(_ => _.Id == id);

        public T Create(T entity)
        {
            var returnEntity = _context.Set<T>().Add(entity).Entity;
            if (AutoCommit) _context.SaveChanges();
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
            if (AutoCommit) _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _context.Set<T>().Remove(GetById(id));
            if (AutoCommit) _context.SaveChanges();
        }
    }

    public interface ICrud<TDto, TEntity>
    {
        IEnumerable<TDto> GetAll();
        TDto GetById(Guid id);
        TDto Create(TDto dto);
        void Update(Guid id, TDto dto);
        void Delete(Guid id);
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
