using System.Threading.Tasks;
using LSG.GenericCrud.DataFillers;
using LSG.GenericCrud.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Sample.DataFiller.Models.DataFillers
{
    public class ByDataFiller : IEntityDataFiller
    {
        public bool IsEntitySupported(EntityEntry entry) => entry.Entity is BaseEntity && (entry.State == EntityState.Added || entry.State == EntityState.Modified);

        public object Fill(EntityEntry entry)
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
            {
                ((BaseEntity) entry.Entity).CreatedBy = "John Doe!";
                ((BaseEntity) entry.Entity).ModifiedBy = "Doe, John!";
            }
            return (BaseEntity)entry.Entity;
        }

        public Task<object> FillAsync(EntityEntry entry)
        {
            return Task.Run(() => Fill(entry));
        }
    }
}
