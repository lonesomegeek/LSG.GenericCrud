using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GenericCrud.Benchmark.Models
{
    public class BenchmarkingContext :
        BaseDbContext,
        IDbContext
    {
        public BenchmarkingContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<HistoricalEvent> Events { get; set; }
        public DbSet<HistoricalChangeset> Changesets { get; set; }
    }
}
