using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LSG.GenericCrud.Repositories.DataFillers
{
    public interface IEntityDataFiller<T>
    {
        bool IsEntitySupported(EntityEntry entry);
        T Fill(EntityEntry entry);
    }
}