using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class TestContext : BaseDbContext, IDbContext
    {
        public TestContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EntityUserStatus> EntityUserStatuses { get; set; }
        public DbSet<HistoricalEvent> HistoricalEvents { get; set; }

    }
}
