# How to customize CRUD layers
This document will explain how you can further customize what is shipped OOTB (out of the box).

Each call that goes through the library controller base class *CrudControllerBase* goes exactly through these 3 layers:
- Controller Layer
- Service Layer
- Repository Layer

Here is sequence diagrams of the actual flow of information:
- For Standard CRUD: [Sequence diagram for standard CRUD](assets/sequence-StandardCrud.png)
- For Historical CRUD: [Sequence diagram for historical CRUD](assets/sequence-HistoricalCrud.png)

If you want to further customize each layers, here is an exhaustive view of how you can customize the interaction of these three layers.

## Controller
When you create your custom controller (ie.: AccountsController), you state that you are different from *CrudControllerBase* class. With ASP.NET Core MVC, you can't go there without creating a custom controller to let MVC know what are the routes and entities you want to publish with your API.

See the [Visual-Studio-Code-Tutorial] for more information.

# Service
When you have special needs, you may need to change the behaviour of the *CrudServiceBase* class. You may want to change/adapt it in many ways, for exemple:
- Inject some custom logging before any actions
- Inject a security layer before the actual call to the repository layer
- Add new functionalities that you do not have in the library and you want it as "base" functionnalities
- ...

To do so, you have a few options:
1. Custom service layer *inheriting* the *CrudServiceBase* class: [Documentation](./CrudServiceBase/Inheritance.md)
2. Custom service layer *implementation* from part of the *ICrudService* interface and part of the *CrudServiceBase* class: [Documentation](./CrudServiceBase/Implementation.md)
3. *Complete* custom service layer *implementation* inheriting from *ICrusService*: [Documentation](./CrudServiceBase/CompleteCustom.md)

As an example, the [DTO custom service layer](../../LSG.GenericCrud.Dto/Services/CrudServiceBase.cs) is a custom service layer of type #2.

Note: All the steps shown also applies to:
- HistoricalCrudServiceBase
- CrudServiceBase (from LSG.GenericCrud.DTO library)
- HistoricalCrudServiceBase (from LSG.GenericCrud.DTO library)

## Repository
When you have special needs, you may need to change the behaviour of the *CrudRepository* class. You may want to change/adapt in may ways, for exemple:
- If you do not want to use Entity Framework Core
    - Because you do not like EFCore
    - For systems that EFCore does not support
- If you want to do your custom implementation of the repository pattern
- ...

TODO: Complete

## How injection works
You can use the builtin injection with three different kind of injections:
- Scoped (AddScoped): Dependency instance created once per client request (connection/session)
- Transient (AddTransient): Dependency instance created each time an object is created with this dependency
- Singleton (AddSingleton): Dependency instance created once and always reused (even in different client sessions)

Further injection documentation from Microsoft: [Documentation](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1#service-lifetimes)

But... This is where it get really interesting. Let's say you have a special need for an entity. This particular *Account* entity (but not the others) is in need for a custom service layer (ie.: CustomServiceLayerWithTransactionApprobation). Here is a sample of what you do today:

```csharp
services.AddCrud();
```
Now with more knowledge about the injection system, you can further customize the specific layer needed for you particular entity.
```csharp
services.AddScoped<ICrudService<Guid, Account>, CustomServiceLayerWithTransactionApprobation<Guid, Account>>();
services.AddCrud();
```
What this code does is:
- When the code will be in need of a service layer for the *Account* entity (ICrudService<Guid, Account>), the injector will inject your custom service layer (CustomServiceLayerWithTransactionApprobation<Guid, Account>)
- When the code will be in need of a service layer for something else (ICrudService<,>), the injector will inject the default service layer (CrudServiceBase<,>)

Note: the <,> means any type of T1 and any type of T2

<!-- References -->
[Visual-Studio-Code-Tutorial]: ../Tutorials/VisualStudioCode.md