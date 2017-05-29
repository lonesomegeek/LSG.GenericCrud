using LSG.GenericCrud.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LSG.GenericCrud.Repositories.DataFillers
{
    public class ByDataFiller : IEntityDataFiller<BaseEntity>
    {
        public bool IsEntitySupported(EntityEntry entry)
        {
            return entry.Entity is BaseEntity && (entry.State == EntityState.Added ||
                                                  entry.State == EntityState.Modified);
        }

        public BaseEntity Fill(EntityEntry entry)
        {
            var entity = ((BaseEntity)entry.Entity);
            entity.CreatedBy = "Not set yet!...";
            entity.ModifiedBy = "Not set yet!...";
            return entity;
        }
    }
}