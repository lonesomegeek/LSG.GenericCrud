using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Sample.CrudOptions.Models
{
    public class MyContext : BaseDbContext, IDbContext
    {
        public MyContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider)
        {
        }

        public DbSet<Account> Accounts { get; set; }
    }
}
