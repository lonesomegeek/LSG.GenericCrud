# Breaking changes from v2.0 to v2.1
## Introduction
Here is the things that have changed in the library from v2.0 to v2.1 that needs to be changed in order to make your code working again!

## Changes
- It is not a **breaking** change. But it is wise to use this implementation instead of the previous for the activation of the Crud/CrudDto services injection.
   - Before:
   ```csharp
    services.AddScoped(typeof(ICrudService<>), typeof(CrudService<>));
    services.AddScoped(typeof(ICrudRepository), typeof(CrudRepository));
    // ...
    ```
   - After:
   ```csharp
    services.AddCrud();
    ```

- DataFillers: To simplify access to any kind of entities, datafillers have changed in a way that they are now agnostic of which entity they are managing.
   - Before:
   *In the application Startup.cs file*
    ```csharp
    services.AddTransient<IEntityDataFiller<BaseEntity>, Models.DataFillers.CustomDataFiller>();
    ```
    *In the CustomDataFiller.cs itself*
    ```csharp
    public class CustomDataFiller : IEntityDataFiller<BaseEntity>
    {
        public bool IsEntitySupported(EntityEntry entry) { /*...*/ }
        public BaseEntity Fill(EntityEntry entry) { /*...*/ }
        public Task<BaseEntity> FillAsync(EntityEntry entry) { /*...*/ }
    }
    ```

   - After:
    *In the application Startup.cs file*
    ```csharp
    services.AddTransient<IEntityDataFiller, Models.DataFillers.CustomDataFiller>();
    ```
    *In the CustomDataFiller.cs itself*
    ```csharp
    public class CustomDataFiller : IEntityDataFiller
    {
        public bool IsEntitySupported(EntityEntry entry) { /*...*/ }
        public object Fill(EntityEntry entry) { /*...*/ }
        public Task<object> FillAsync(EntityEntry entry) { /*...*/ }
    }
    ```