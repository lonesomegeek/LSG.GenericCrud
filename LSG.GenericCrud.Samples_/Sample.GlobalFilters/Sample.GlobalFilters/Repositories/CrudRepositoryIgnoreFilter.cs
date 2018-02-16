using System.Collections.Generic;
using System.Linq;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Sample.GlobalFilters.Repositories
{
    public class CrudRepositoryIgnoreFilter : CrudRepository
    {
        private readonly IDbContext _context;

        public CrudRepositoryIgnoreFilter(IDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAllIgnoreFilter<T>() where T : class, IEntity, new()
        {
            return _context.Set<T>().IgnoreQueryFilters().ToList();
        }
    }
}