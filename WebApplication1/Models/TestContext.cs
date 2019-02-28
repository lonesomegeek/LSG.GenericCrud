using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.EntityFrameworkCore;
using NUlid;

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
        public DbSet<HistoricalChangeset> HistoricalChangesets { get; set; }


        public DbSet<MyIntEntity> IntEntities { get; set; }
        public DbSet<MyUlidEntity> UlidEntities { get; set; }
    }

    public class MyIntEntity : IEntity<int>
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class MyUlidEntity : IEntity<string>
    {
        public MyUlidEntity()
        {
            Id = Ulid.NewUlid().ToString();
        }

        public string Id { get; set; }
        public string Value { get; set; }
    }
}
