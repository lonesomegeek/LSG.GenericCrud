using System;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LSG.GenericCrud.TestApi.Models
{
    public class MyContext : BaseDbContext, IDbContext
    {
        public MyContext
            (DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Carrot> Carrots{ get; set; }
        public DbSet<HistoricalEvent> HistoricalEvents { get; set; }
    }
}
