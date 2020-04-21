# Feature: Data Fillers
I've added the data filler feature to be able to easily add any kind of data automatically to entities. In the document [Feature: Historical Crud](HistoricalCrud.md), I've talked about *the who?* and *the when?* that were not covered natively by historical crud, these kind of informations can be automatically filled by the data fillers.

Here is a link to a DataFiller source code sample: [Link](https://github.com/lonesomegeek/LSG.GenericCrud/tree/version/4.1.1/LSG.GenericCrud.Samples/Sample.DataFiller/Sample.DataFiller)

## What is happening behind the scenes
With the big help of injection, each time an interaction is required with an entity (POST/PUT verbs) the DAL (Data Access Layer) will check for each entity that is in need of being processed. The DAL will check if there exists datafiller(s) that need to be executed on the specified entity type. You can take a look to the library source code [here](https://github.com/lonesomegeek/LSG.GenericCrud/blob/master/LSG.GenericCrud/Repositories/BaseDbContext.cs)

## How to enable "data fillers"
Two steps are required to enable data fillers:
- Create your data filler
- Add an injection specification

### Create a DateTimeDataFiller
You need to create a class that will implements *IEntityDataFiller\<,>*. This interface is used by the DAL to detect data fillers. Type *T* can be:
- An abstract class (generic)
- A specific type
- An interface
- ...

In this sample, I want to store at what time the entity is created and afterward, when it is modified. Here is the class you need:

```csharp
public class DateDataFiller : IEntityDataFiller<BaseEntity>
{
    public bool IsEntitySupported(EntityEntry entry) {
        return entry.Entity is BaseEntity && 
            (entry.State == EntityState.Added || entry.State == EntityState.Modified);
    }
    
    public BaseEntity Fill(EntityEntry entry)
    {
        if (entry.State == EntityState.Added) ((BaseEntity)entry.Entity).CreatedDate = DateTime.Now;
        ((BaseEntity)entry.Entity).ModifiedDate = DateTime.Now;
        return (BaseEntity)entry.Entity;
    }
}
```

You need to implement two methods:
- IsEntitySupported: Returns true if the datafiller can fill data for the specified entity
- Fill: Fill information on the supported entity.

### Add Datafiller in application services (injection)
To enable your new datafiller, you need to adapt *Startup.cs* class, *ConfigureServices(...)* method and add:

```csharp
services.AddTransient<IEntityDataFiller<BaseEntity>, DateDataFiller>();
```