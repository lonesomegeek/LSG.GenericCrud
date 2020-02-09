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