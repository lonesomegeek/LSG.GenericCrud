# Feature: Entity history tracking @ Historical CRUD
I've been in many scenario when I was asked to track the evolution of an entity through time in an application. This feature simplifies the acquisition of:
- the what? What has changed, What action as been executed 
- *the when?* When did the action occured
- *the who*? Who did the manipulation on the entity

**Note:** The *when?* and the *who?* is not supported natively by HistoricalCrudController\<T> but is covered by [Feature: DataFiller\<T>](./FeatureDataFillers.md).

The history tracking feature enables automatic history tracking with few modifications to existing controllers CrudController\<T>.

## What is happening behind the scenes
Before getting into further details, I think this is important for you to understand what is happening behind the scenes.

First of all, when you will define a controller inheriting from HistoricalCrudController\<T>, it will absolutely need a compatible data access layer (DAL) that supports historical transactions. If you do not specifies a compatible *history-ready* DAL to your HistoricalCrudController\<T>, your entity modifications will be saved, but not the history trackng.

Also, the historical events are saved aside of the entities. That means that all historical events of all kind on all entities will be stored in the same table.

Actions that are tracked by HistoricalCrudControllee\<T>:
- Create: First event tracked obviously when the entity is created
- Update: All update events that may occur after the creation
- Delete: This event can only occur once (or never) and it marks the deletion event of the entity

You should know that the entity transaction and history transaction occurs at the same time (same database transaction). If one of the two fails, no database action will occur. Sweet eh (logical I say =P)!?

## How to make it "Historical"
To enable the *Historical* feature, you have few steps to do to make the thing work:
- Change your controller definition to inherit from HistoricalCrudController\<T>
- Change your controller constructor to be injected an *Historical-ready* repository (data access layer @ dal)
- Adapt your database context to support *HistoricalEvents* dataset
- Adapt DAL injection

### Controller adaptation
Here is a configuration with CrudController\<T> controller:

```csharp
[Route("api/[controller]")]
public class AccountsController : CrudController<Account>
{
    public AccountsController(ICrudService<Account> service) : base(service)
}
```

Here is a configuration with HistoricalCrudController\<T> controller:
```csharp
[Route("api/[controller]")]
public class AccountsController : HistoricalCrudController<Account>
{
    public AccountsController(IHistoricalCrudService<Account> service) : base(service)
}
```

### Context adaptation
Adjust your existing context class to include this property:
```csharp
public DbSet<HistoricalEvent> HistoricalEvents { get; set; }
public DbSet<HistoricalChangeset> HistoricalChangesets { get; set; }

```
This inclusion will enables the HistoricalCrud\<T> DAL to do the tracking of all the events. In future release, I may put settings to let you choose where to drop the entity events (up to you).

### DAL injection adaptation
In **Startup.cs**, add a new injection configuration in *ConfigureServices(...)*
```csharp
services.AddScoped(typeof(IHistoricalCrudService<>), typeof(HistoricalCrudService<>));
```

## It is a CrudController\<T> plus...
This is programatically-talking a CrudController\<T> with history tracking but with more routes. As a reminder, here is the default routes provided with a CrudController\<T>:

| 	 | Verb    |	Route	                                 | Results   | Description |
|----|----------|--------------------------------------------|-----------|-------------|
| C  |	GET     | /[entity]	                                 | 200	     | Retreive all objects |
| C  |	GET     | /[entity]/:id	                             | 200,404	 | Retreive one object |
| C  |	HEAD    | /[entity]/:id	                             | 204,404	 | Get an indication of the existance of an object |
| C  |	POST    | /[entity]	                                 | 201,400	 | Create an object |
| C  |	PUT     | /[entity]/:id	                             | 204	     | Update an object |
| C  |	DELETE  | /[entity]/:id	                             | 200,404	 | Delete an object |
| C  |	POST    | /[entity]/:id/copy	                     | 201,404	 | Copy active version of an object in a new object |

You will get more routes with an HistoricalCrudController\<T>:

| 	 | Verb    |	Route	                                 | Results   | Description |
|----|----------|--------------------------------------------|-----------|-------------|
| HC |	GET	    | /[entity]/:id/history	                     | 200,404	 | Get transaction history of an object |
| HC |	POST    | /[entity]/:id/restore	                     | 201,404	 | Restore a deleted object in a new object |
| HC |	POST    | /[entity]/:entityId/restore/:changesetId	 | 201,404	 | Restore a version of an object in the same object |
| HC |	POST    | /[entity]/:entityId/copy/:changesetId	     | 201,404	 | Copy a version of an object in to a new object |
| HC |	GET	    | /[entity]/read-status	                     | 200	     | Retreive all object with their read status |
| HC |	GET	    | /[entity]/:id/read-status	                 | 200	     | Retreive one object with its read status |
| HC |	POST    | /[entity]/read	                         | 201	     | Mark all objects as "read" |
| HC |	POST    | /[entity]/:id/read                     	 | 201,404	 | Mark one object as "read" |
| HC |	POST    | /[entity]/unread	                         | 201	     | Mark all object as "unread" |
| HC |	POST    | /[entity]/:id/unread	                     | 201,404	 | Mark one object as "unread" |
| HC |	POST    | /[entity]/:id/delta	                     | 201,404	 | Extract change delta of one object |

## Samples

Here is a link to an historical crud source code sample: [Link](https://github.com/lonesomegeek/LSG.GenericCrud/blob/master/LSG.GenericCrud.Samples/Sample.HistoricalCrud/README.md)
