using System;
using LSG.GenericCrud.Repositories;
using Microsoft.EntityFrameworkCore;
using Sample.DataFiller.Models.Entities;

namespace Sample.DataFiller.Models
{
    public class SampleContext : BaseDbContext, IDbContext
    {
        public SampleContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider) { }

        public DbSet<Account> Accounts { get; set; }
    }
}
