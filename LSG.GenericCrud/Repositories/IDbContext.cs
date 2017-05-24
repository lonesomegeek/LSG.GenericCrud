using Microsoft.EntityFrameworkCore;

namespace LSG.GenericCrud.Repositories
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
    }
}
