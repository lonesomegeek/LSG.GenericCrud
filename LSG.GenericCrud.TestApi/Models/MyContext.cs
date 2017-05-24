using LSG.GenericCrud.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LSG.GenericCrud.TestApi.Models
{
    public class MyContext : DbContext, IDbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
    }
}
