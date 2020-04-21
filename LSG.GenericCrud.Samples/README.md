
# Introduction
This demo is a simple web app to manage items you have at home and with who you have shared them. It is also do show you how to integrate entities managed by the LSG.GenericCrud with a frontend application. This sample include these entities:

| Type        | Type of controller       | Type of implementation | DTO |
|-------------|--------------------------|------------------------|-----|
| [Contact]     | CrudController           | Implemented            | No  |
| [Item]        | CrudController           | Implemented            | Yes |
| [Share]       | HistoricalCrudController | [CustomService]          | No  |
| [Contributor] | CrudController           | Inherited              | No  |
| [User]        | HistoricalCrudController | Inherited              | Yes |
| [Hook]        | CrudController           | Inherited              | No  |
| [BlogPost]    | HistoricalCrudController | Implemented            | Yes |

There is a [CustomService] layer that will act as a webhook handler for entities. When a specific action configured hits an endpoint, if a webhook is configured, it will be trigger.

<!-- References -->
[Contact]: Controllers/ContactsController.cs
[Item]: Controllers/ItemsController.cs
[Share]: Controllers/SharesController.cs
[Contributor]: Controllers/ContributorsController.cs
[User]: Controllers/UsersController.cs
[Hook]: Controllers/HooksController.cs
[BlogPost]: Controllers/BlogPostsController.cs
[CustomService]: Services/CustomCrudService.cs