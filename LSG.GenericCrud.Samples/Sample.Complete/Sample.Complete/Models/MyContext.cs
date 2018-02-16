using System;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.EntityFrameworkCore;
using Sample.Complete.Models.Entities;

namespace Sample.Complete.Models
{
    public class MyContext : BaseDbContext, IDbContext
    {
        public MyContext
            (DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider)
        {
        }

        public DbSet<HistoricalEvent> HistoricalEvents { get; set; }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
