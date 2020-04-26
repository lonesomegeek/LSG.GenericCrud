Here is a sample of how to "inherit" from *CrudServiceBase* code base in a custom service layer.

```csharp
public class CustomInheritedCrudService<T1, T2> : CrudServiceBase<T1, T2> where T2 : class, IEntity<T1>, new()
{
    public CustomInheritedCrudService(ICrudRepository repository) : base(repository) { }

    public override Task<T2> CopyAsync(T1 id) => base.CopyAsync(id);
    public override Task<T2> CreateAsync(T2 entity) => base.CreateAsync(entity);
    public override Task<T2> DeleteAsync(T1 id) => base.DeleteAsync(id);
    public override Task<IEnumerable<T2>> GetAllAsync() => base.GetAllAsync();
    public override Task<T2> GetByIdAsync(T1 id) => base.GetByIdAsync(id);
    public override Task<T2> UpdateAsync(T1 id, T2 entity) => base.UpdateAsync(id, entity);        
}
```

Here is a better overview of what you can do before and after each action with this sequence diagram. Please note that with this technique, you can do whateaver you want before or after the *base* action, but, you can't change the *base* behaviour.

[<img src="../assets/sequence-StandardCrud_CustomService.png">](../assets/sequence-StandardCrud_CustomService.png)

```csharp
public class CustomInheritedCrudService<T1, T2> : CrudServiceBase<T1, T2> where T2 : class, IEntity<T1>, new()
{
    public CustomInheritedCrudService(ICrudRepository repository) : base(repository) { }

    public override Task<T2> CopyAsync(T1 id) {
        // 1: execute before action    
        var result = base.CopyAsync(id);
        // 2: execute after action
        return result; // or altered result from step "2"
    }

    public override Task<T2> CreateAsync(T2 entity) {
        // 1: execute before action
        var result = base.CreateAsync(entity);
        // 2: execute after action
        return result; // or altered result from step "2"
    }

    public override Task<T2> DeleteAsync(T1 id) {        
        // 1: execute before action
        var result = base.DeleteAsync(id);
        // 2: execute after action
        return result; // or altered result from step "2"
    }

    public override Task<IEnumerable<T2>> GetAllAsync() {
        // 1: execute before action
        var result = base.GetAllAsync();
        // 2: execute after action
        return result; // or altered result from step "2"
    }

    public override Task<T2> GetByIdAsync(T1 id) {
        // 1: execute before action
        var result = base.GetByIdAsync(id);
        // 2: execute after action
        return result; // or altered result from step "2"
    }

    public override Task<T2> UpdateAsync(T1 id, T2 entity) {
        // 1: execute before action
        var result = base.UpdateAsync(id, entity);        
        // 2: execute after action
        return result; // or altered result from step "2"
    }
}
```