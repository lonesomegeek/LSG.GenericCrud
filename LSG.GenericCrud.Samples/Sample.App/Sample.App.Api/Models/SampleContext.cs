using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Repositories;
using Microsoft.EntityFrameworkCore;
using Sample.App.Api.Controllers;

namespace Sample.App.Api.Models
{
    public class SampleContext : BaseDbContext, IDbContext
    {
        public SampleContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider)
        {
        }

        public DbSet<Friend> Friends { get; set; }
        public DbSet<Car> Cars { get; set; }
    }
}
