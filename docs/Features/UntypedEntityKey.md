# Feature: Untyped entity key
When I've designed my first version of the library (before 1.*), all my personal designs were around *Guid* entity type. At first, it was working really well. By the time I was using my library personaly, people started complaining of this regidity. I've decided to redesign all the layers to take this recommendation and implement it.

To let my library manage a beautiful and power CRUD with multiple feature in the background, managed entities needs to implement my *IEntity<T>* interface where *<T>* can be anything of simple type:
- string
- guid
- int
- float
- ...

But it **cannot** be of complex type *MyObject* for example. Why? Because of the limitation of EntityFramework. EF can't manage complex types in a table column by default. If you need to manage complex typed keys, it need to be transtyped to a simple type before beeing sent to EntityFramework.

Here is a link to the untyped entity key type code sample: [Link](https://github.com/lonesomegeek/LSG.GenericCrud/tree/version/4.1.1/LSG.GenericCrud.Samples/Sample.Complete/Sample.Complete/Controllers/AccountsController.cs)

> Note: I've made a sample with a complex type that hacks into a string. Check my samples for ULID type [Link](https://github.com/lonesomegeek/LSG.GenericCrud/tree/version/4.1.1/LSG.GenericCrud.Samples/Sample.Complete/Sample.Complete/Controllers/UlidController.cs), thanks to this .NET implementation provided by [RobThree](https://github.com/RobThree/NUlid)