# Feature: Async pipeline
Note: Please note that not 100% of the pipeline is asynchronous. I've made an effort of doing async for all the operations that were costing the much (ie: Entity Framework Related Stuff, save, create, delete).

I've added the async feature to let you use if you want synchronous or asynchronous Controllers and Repositories (Data Access Layers @ DAL). TODO: WHY USE

## What is happening behind the scenes
Each time a request enters a controller, it is redirected to an async DAL execution. 

## How to use "async pipeline"
Two steps are required to enable data fillers:
- Change the inherited controller type 
- If needed, create an async version of your DataFiller implementations

### Use of an async controller
You need to create a class that will inherit from *Crud**Async**Controller\<T>* or *HistoricalCrud**Async**Controller\<T>*. From there, all the requests sent to the API Controller will be managed aynchronously.

Sample:

```csharp
    public class MyAsyncController : CrudAsyncController<MyObject>
    {
        public MyAsyncController(ICrudService<MyObject> service) : base(service)
        {
        }
    }
```

### Implement an async version of a data filler
You need to implement the async version of a the Fill method of a DataFiller only if you use asynchronous controllers. If you do not use asynchronos controllers, you can leave this method with the default ```throw new NotImplementedException();```

## Samples

Here is a link to the Asynchronous source code sample: [Link](https://github.com/lonesomegeek/LSG.GenericCrud.Samples/tree/master/Sample.Async)
