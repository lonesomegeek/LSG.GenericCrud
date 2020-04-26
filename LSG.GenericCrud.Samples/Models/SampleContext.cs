using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using LSG.GenericCrud.Samples.Models.Entities;

namespace LSG.GenericCrud.Samples.Models
{
    public class SampleContext :
        BaseDbContext,
        IDbContext
    {
        public SampleContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider) {}
        public DbSet<HistoricalChangeset> HistoricalChangesets { get; set; }
        public DbSet<HistoricalEvent> HistoricalEvents { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Contributor> Contributors { get; set; }
        public DbSet<Entities.User> Users { get;set; }
        public DbSet<Hook> Hooks { get;set; }
        public DbSet<BlogPost> BlogPosts { get;set; }

    }
}
