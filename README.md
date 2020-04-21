# LSG.GenericCrud
[![NuGet](https://img.shields.io/nuget/dt/LSG.GenericCrud.svg)](https://www.nuget.org/packages/LSG.GenericCrud)
[![NuGet](https://img.shields.io/nuget/v/LSG.GenericCrud.svg)](https://www.nuget.org/packages/LSG.GenericCrud)
[![Board Status](https://dev.azure.com/lonesomegeek/ddef9b23-a7fc-461f-ade2-bcb9e4e6e579/e5c916ea-37ad-4bba-9ab4-ad6a5070fe19/_apis/work/boardbadge/3c551e06-0bb9-4488-8681-5d1b7cbbaea2)](https://dev.azure.com/lonesomegeek/ddef9b23-a7fc-461f-ade2-bcb9e4e6e579/_boards/board/t/e5c916ea-37ad-4bba-9ab4-ad6a5070fe19/Microsoft.RequirementCategory)
[![GitHub license](https://img.shields.io/github/license/lonesomegeek/LSG.GenericCrud.svg)](https://github.com/lonesomegeek/LSG.GenericCrud/blob/master/LICENSE)

# TL;DR - Getting started / Tutorials

This library provides many built-in ASP.NET Core Web API routes for simplified and [DRY] REST Apis!

Simpliest scenarios:
- Using Visual Studio Code (or command line): [Tutorial](docs/Tutorials/VisualStudioCode.md)
- Using Visual Studio (2019 v16.4+): [Tutorial](docs/Tutorials/VisualStudio.md)

Demo app built on the library:
- URL: http://genericcrud-demo.lonesomegeek.com
- [Documentation](LSG.GenericCrud.Samples/README.md)

# Introduction
This library is used to provide simplified RESTful [CRUD] apis with a multilayer architecture:
- Generic CRUD Controller layer
- Generic CRUD Service layer
- Generic CRUD Repository layer 
- -== **NEW** ==- More information on [how to customize each layers](./docs/CustomLayers/README.md)

This library allies injection (IoC), minimum codebase for maximum efficiency.

Obviously, when you are working with databases and entities, you need to write some code to be able to manage these entities. And, when working with multiple kind of entities, you need to rewrite code that is similar for each entities. This library is getting rid of the duplicated code and keeps everything **DRY**. Here is what the library is able to do!

| 	 | Verb    |	Route	                                 | Results   | Description |
|----|----------|--------------------------------------------|-----------|-------------|
| C  |	GET     | /[entity]	                                 | 200	     | Retreive all objects |
| C  |	GET     | /[entity]/:id	                             | 200,404	 | Retreive one object |
| C  |	HEAD    | /[entity]/:id	                             | 204,404	 | Get an indication of the existance of an object |
| C  |	POST    | /[entity]	                                 | 201,400	 | Create an object |
| C  |	PUT     | /[entity]/:id	                             | 204	     | Update an object |
| C  |	DELETE  | /[entity]/:id	                             | 200,404	 | Delete an object |
| C  |	POST    | /[entity]/:id/copy	                     | 201,404	 | Copy active version of an object in a new object |
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

Legend
| Code | Description |
|------|-------------|
| C    | Feature available in CrudController |
| HC   | Feature available in HistoricalCrudController |
--Enjoy!

# Prerequisites
You need:
- [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1) (for any of the options below)

You also need one of these options below:
- [Visual Studio](https://www.visualstudio.com/downloads/) ([Visual Studio 2019 - v16.4+](https://devblogs.microsoft.com/dotnet/announcing-net-core-3-1/))
- [Visual Studio Code](https://code.visualstudio.com/)
- Notepad ;)

# Features supported, by library

I've designed this library to be pretty extensible. Here is some features supported and links to further documentation:

- [LSG.GenericCrud](https://www.nuget.org/packages/LSG.GenericCrud/) 
    - Untyped entity key: [More details](./docs/Features/UntypedEntityKey.md)
    - Entity history tracking: [More details](./docs/Features/HistoricalCrud.md)
        - Copy of an existing object into another one
        - Readeable CRUD, to let you know personaly (as a user) if something has changed since last view
    - Data fillers: [More details](./docs/Features/DataFillers.md)
    - Customize each layers: [More details](./docs/CustomLayers/README.md)
- [LSG.GenericCrud.Dto](https://www.nuget.org/packages/LSG.GenericCrud.Dto/)
    - DTO to Entity (and Entity to DTO) mapping for Crud\<T> or HistoricalCrud\<T> controllers: [More details](./docs/Features/DTO.md)

# Release notes
- [v5.0 Release Notes](docs/ReleaseNotes/v5.0.md)
- [v4.1 Release Notes](docs/ReleaseNotes/v4.1.md)
- [v4.0 Release Notes](docs/ReleaseNotes/v4.0.md)
- [v3.0 Release Notes](docs/ReleaseNotes/v3.0.md)

# Breaking changes
- [From v4.0 to v5.0](docs/BreakingChanges/From-v4.0-to-v5.0.md)
- [From v3.0 to v4.0](docs/BreakingChanges/From-v3.0-to-v4.0.md)
- [From v2.1 to v3.0](docs/BreakingChanges/From-v2.1-to-v3.0.md)
- [From v2.0 to v2.1](docs/BreakingChanges/From-v2.0-to-v2.1.md)
- [From v1.* to v2.*](docs/BreakingChanges/From-v1-to-v2.md)
- [Previous notes](docs/ReleaseNotes/OldReleaseNotes.md)

<!-- References -->

[CRUD]: https://en.wikipedia.org/wiki/Create,_read,_update_and_delete
[DRY]: https://en.wikipedia.org/wiki/Don%27t_repeat_yourself