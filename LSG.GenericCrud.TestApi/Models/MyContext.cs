using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.DAL;
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
