using System;
using System.Linq;
using LSG.GenericCrud.Repositories;
using Microsoft.EntityFrameworkCore;
using Sample.GlobalFilters.Repositories;

namespace Sample.GlobalFilters.Models
{
    public class SampleContext : BaseDbContext, IDbContext
    {
        private readonly IUserInfoRepository _userInfoRepository;

        public DbSet<Item> Items { get; set; }
        public DbSet<Car> Cars { get; set; }

        public SampleContext(DbContextOptions options, IServiceProvider serviceProvider, IUserInfoRepository userInfoRepository) : base(options, serviceProvider)
        {
            _userInfoRepository = userInfoRepository;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasQueryFilter(_ => !_.IsDeleted);
            modelBuilder.Entity<Car>().HasQueryFilter(_ => _.TenantId == _userInfoRepository.TenantId);
        }
    }
}
