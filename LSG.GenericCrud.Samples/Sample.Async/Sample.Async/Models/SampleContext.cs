using System;
using LSG.GenericCrud.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Sample.Async.Models
{
    public class SampleContext : BaseDbContext, IDbContext
    {
        public SampleContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider) { }

        public DbSet<Account> Accounts { get; set; }
    }
}