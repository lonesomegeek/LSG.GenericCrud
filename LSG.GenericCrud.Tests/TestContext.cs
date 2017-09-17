using System;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LSG.GenericCrud.Tests
{
    public class TestContext : BaseDbContext, IDbContext
    {
        public TestContext
            (DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider)
        {
        }

        public DbSet<TestEntity> TestEntities { get; set; }
        public DbSet<HistoricalEvent> HistoricalEvents { get; set; }
    }
}