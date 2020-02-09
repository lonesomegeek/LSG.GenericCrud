Each call that goes through the library controller base class *CrudControllerBase*, goes exactly through these 3 layers:
- Controller Layer
- Service Layer
- Repository Layer

Here is an exhaustive view of how you can customize the way you want the interaction with these three layers.

## Controller
When you create your custom controller (ie.: AccountsController), you state that you are differente from *CrudControllerBase* class. With ASP.NET Core MVC, you can't go there without creating a custom controller to let MVC know what are the routes and entities you want to publish with your API.

Here is the flow:
- TODO: Insert flow here

# Service
When you have special needs, you may need to change the behaviour of the *CrudServiceBase* class. You may want to change/adapt it in many ways, for exemple:
- Inject some custom logging before any actions
- Inject a security layer before the actual call to the repository layer
- Add new functionalities that you do not have in the library and you want it as "base" functionnalities
- ...

To do so, you have a few options:
- Custom service layer from inheritance of the *CrudServiceBase* class: [Documentation](./CustomServiceLayerInheritance.md)
- Custom service layer implementation from part of the *ICrudService* interface and part of the *CrudServiceBase* class: [Documentation](./CustomServiceLayerImplementation.md)
- Complete custom service layer implementation inheriting from *ICrusService*
Note: All the steps shown also applies to:
- HistoricalCrudServiceBase
- CrudServiceBase (from LSG.GenericCrud.DTO library)
- HistoricalCrudServiceBase (from LSG.GenericCrud.DTO library)

Inheritance vs Implementation
## Inheritance
You "Inherit/Resuse" code from "Base" class.
Advantages:
- fewer code to write
Inconvenients:
- less flexible, you are stuck with the code provided

## Repository
When you have special needs, you may need to change the behaviour of the *CrudRepository* class. You may want to change/adapt in may ways, for exemple:
- If you do not want to use Entity Framework Core
    - Because you do not like EFCore
    - For systems that EFCore does not support
- If you want to do your custom implementation of the repository pattern
- ...

TODO: Complete
    
    - 

## How injection works
You can use the builtin injection with three different kind of injections:
- Scoped (AddScoped): TODO
- Transient (AddTransient): TODO
- Singleton (AddSingleton): TODO

But... This if where it get really interesting. Let's say you have a special need for an entity. This particular *Account* entity (but not the others) is in need for a custom service layer (ie.: CustomServiceLayerWithTransactionApprobation). Here is a sample of what you do today:
```csharp
services.AddCrud();
```
```csharp
services.AddScoped<ICrudService<Guid, Account>, CustomServiceLayerWithTransactionApprobation<Guid, Account>>();
services.AddCrud();
```
What this code does is:
- When the code will be in need of a service layer for the *Account* entity (ICrudService<Guid, Account>), the injector will inject your custom service layer (CustomServiceLayerWithTransactionApprobation<Guid, Account>)
- When the code will be in need of a service layer for something else (ICrudService<,>), the injector will inject the default service layer (CrudServiceBase<,>)
Note: the <,> means any type of T1 and any type of T2