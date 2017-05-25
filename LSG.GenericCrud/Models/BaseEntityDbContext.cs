using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LSG.GenericCrud.Models
{
    public class BaseEntityDbContext : DbContext
    {
        public BaseEntityDbContext(DbContextOptions options) : base(options) { }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            // TODO: Change for injected function/service for user extraction (case for SAML/OAuth/... supports)
            var currentUsername = "Not set yet!";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedDate = DateTime.Now;
                    ((BaseEntity)entity.Entity).CreatedBy = currentUsername;
                }

                ((BaseEntity)entity.Entity).ModifiedDate = DateTime.Now;
                ((BaseEntity)entity.Entity).ModifiedBy = currentUsername;
            }

            return base.SaveChanges();
        }
    }
}
