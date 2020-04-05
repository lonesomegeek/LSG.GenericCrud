using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Samples.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using LSG.GenericCrud.Samples.Models.Entities;
using Object = LSG.GenericCrud.Samples.Models.Entities.Object;

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
        public DbSet<Object> Objects { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<Contact> Contacts { get; set; }

    }
}
