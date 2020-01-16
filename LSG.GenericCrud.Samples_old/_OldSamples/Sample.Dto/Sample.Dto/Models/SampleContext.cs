using System;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.EntityFrameworkCore;
using Sample.Dto.Models.Entities;

namespace Sample.Dto.Models
{
    public class SampleContext : BaseDbContext, IDbContext
    {
        public SampleContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<HistoricalEvent> HistoricalEvents { get; set; }
    }
}
