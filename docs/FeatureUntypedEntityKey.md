# Feature: Untyped entity key
When I've designed my first version of the library (before 1.*), all my personal designs were around *Guid* entity type. At first, it was working really well. By the time I was using my library personaly, people started complaining of the regidity it comes with. I've decided to redesign all the layers to take this recommendation and implement it.

To let my library manage a beautiful and power CRUD with multiple feature in the background, managed entities needs to implement my *IEntity<T>* interface. *<T>* can be anything of simple type:
- string
- guid
- int
- float
- ...

But it **cannot** be of complex type *MyObject* for example. Why? Because of the limitation of EntityFramework. EF can't manage complex types in a table column by default.

## What is happening behind the scenes
Just because you are using *IEntity<T>*, all the magics operate in the backend to let my strong typings doing the rest.

## How to use IEntity
You are able to use those two ways for your entities:
- IEntity: Will operates as a IEntity<Guid>
- IEntity<T>: Will try to implement *IEntity* interface with type *T* as the key type

As you can see, for sake of simplicity and for retrocompatibility, you are able to use the old method *IEntity*. It will just be overrided behind the scenes for *IEntity<Guid>*.

## Samples

Here is a link to the untyped entity key type code sample: [Link](https://github.com/lonesomegeek/LSG.GenericCrud/blob/master/LSG.GenericCrud.Samples/Sample.Complete/Sample.Complete/Controllers/AccountsController.cs)

> Note: I've made a sample with a complex type that hacks into a string. Check my samples for ULID type [Link](https://github.com/lonesomegeek/LSG.GenericCrud/blob/master/LSG.GenericCrud.Samples/Sample.Complete/Sample.Complete/Controllers/UlidController.cs), thanks to this .NET implementation provided by [RobThree](https://github.com/RobThree/NUlid)