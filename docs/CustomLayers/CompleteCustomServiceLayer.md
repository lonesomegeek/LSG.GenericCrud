Here is a sample of what to do if you want to have complete control over your service layer. With that method you can bypass all *base* code of my library. This method is **really** similar to the [Custom Service Layer Implementation](./CustomServiceLayerImplementation.md) at the exception that there is no forwaring (proxy) to my library.

```csharp
public class CompleteCustomImplementedCrudService<T1, T2> : ICrudService<T1, T2> where T2 : class, IEntity<T1>, new()
{
    public CompleteCustomImplementedCrudService() { }

    public bool AutoCommit { get; set; }

    public async Task<T2> CopyAsync(T1 id) => throw new NotImplementedException();
    public async Task<T2> CreateAsync(T2 entity) => throw new NotImplementedException();
    public async Task<T2> DeleteAsync(T1 id) => throw new NotImplementedException();
    public async Task<IEnumerable<T2>> GetAllAsync() => throw new NotImplementedException();
    public async Task<T2> GetByIdAsync(T1 id) => throw new NotImplementedException();
    public async Task<T2> UpdateAsync(T1 id, T2 entity) => throw new NotImplementedException();
}
```