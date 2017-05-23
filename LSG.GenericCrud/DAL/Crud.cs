using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace LSG.GenericCrud.DAL
{
    public class Crud<T> : ICrud<T>
        where T : class, IEntity, new()
    {
        private readonly IDbContext _context;

        public Crud(IDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll() => _context.Set<T>().AsEnumerable();

        public T GetById(Guid id) => _context.Set<T>().SingleOrDefault(_ => _.Id == id);
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
        //T Create(T entity);
        //void Update(Guid id, T entity);
        //void Delete(Guid id);
    }
}
