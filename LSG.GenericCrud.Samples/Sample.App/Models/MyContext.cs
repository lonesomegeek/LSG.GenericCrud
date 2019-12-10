using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.App.Models
{
    public class MyContext : BaseDbContext, IDbContext
    {
        public MyContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider)
        {
        }

        public DbSet<Item> Items { get; set; }

        public DbSet<HistoricalChangeset> HistoricalChangesets { get; set; }
        public DbSet<HistoricalEvent> HistoricalEvents { get; set; }
    }
}
