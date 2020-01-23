using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Samples.Models
{
    public class SampleContext :
        BaseDbContext,
        IDbContext
    {
        public SampleContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider)
        {
        }
        public DbSet<HistoricalChangeset> HistoricalChangesets { get; set; }
        public DbSet<HistoricalEvent> HistoricalEvents { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
