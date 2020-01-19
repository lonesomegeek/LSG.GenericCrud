using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Samples.Controllers;
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

        public DbSet<Account> Accounts { get; set; }
    }
}
