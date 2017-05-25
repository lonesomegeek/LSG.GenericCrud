using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LSG.GenericCrud.TestApi.Models
{
    public class MyContext : BaseEntityDbContext, IDbContext
    {
        public MyContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
    }
}
