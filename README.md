# LSG.GenericCrud
[![NuGet](https://img.shields.io/nuget/dt/LSG.GenericCrud.svg)](https://www.nuget.org/packages/LSG.GenericCrud)
[![NuGet](https://img.shields.io/nuget/v/LSG.GenericCrud.svg)](https://www.nuget.org/packages/LSG.GenericCrud)
[![GitHub issues](https://img.shields.io/github/issues/lonesomegeek/LSG.GenericCrud.svg)](https://github.com/lonesomegeek/LSG.GenericCrud/issues)
[![GitHub license](https://img.shields.io/github/license/lonesomegeek/LSG.GenericCrud.svg)](https://github.com/lonesomegeek/LSG.GenericCrud/blob/master/LICENSE)

# TL;DR - Getting started / Tutorials

This library provides many built-in ASP.NET Core Web API routes for simplified and DRY REST Apis!

Simpliest scenarios:
- Using Visual Studio Code (or command line): [Tutorial](docs/1_TutorialAcocuntCrudVisualStudioCode.md)
- Using Visual Studio (2017 update 9+ - v15.9+): [Tutorial](docs/1_TutorialAcocuntCrudVisualStudio.md)

Want to have a look to *more samples*, take a look at [these samples](LSG.GenericCrud.Samples/README.md)

> Before diving into more complex scenarios (and if you want to understand the underlying architecture), you should take a rapid view at [this explanation of the new architecture in v3.*](./docs/ReleaseNotes-v3.0.md).

# Introduction
This library is used to provide simplified RESTful [CRUD](https://en.wikipedia.org/wiki/Create,_read,_update_and_delete) apis with a multilayer architecture:
- Generic CRUD Controller layer
- Generic CRUD Service layer
- Generic CRUD Repository layer 

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
- [.NET Core 2.2 SDK](https://dotnet.microsoft.com/download/dotnet-core/2.2) (for any of the options below)

You also need one of these options below:
- [Visual Studio](https://www.visualstudio.com/downloads/), at least a version that supports aspnetcore 2.2 (Visual Studio 2017 Update 9+ - v15.9+)
- [Visual Studio Code](https://code.visualstudio.com/)
- For superheroes: A command line, notepad, and some will-power =P

# Features supported, by library

I've designed this library to be pretty extensible. Here is some features supported and links to further documentation:

- [LSG.GenericCrud](https://www.nuget.org/packages/LSG.GenericCrud/) 
    - Untyped entity key: [More details](./docs/FeatureUntypedEntityKey.md) TODO
    - Entity history tracking: [More details](./docs/FeatureHistoricalCrud.md)
        - Copy of an existing object into another one
        - Readeable CRUD, to let you know personaly (as a user) if something has changed since last view
    - Data fillers: [More details](./docs/FeatureDataFillers.md)
- [LSG.GenericCrud.Dto](https://www.nuget.org/packages/LSG.GenericCrud.Dto/)
    - DTO to Entity (and Entity to DTO) mapping for Crud\<T> or HistoricalCrud\<T> controllers: [More details](./docs/FeatureDTO.md)

> Note that all libraries supports customized layer for each layer: *more details to come*

Future ideas that can/will eventually be supported (feel free to help if you want!):
- Per entity security policies
- Complete web app with api samples
- *and more*...

# Breaking changes
- [From v3.0 to v4.0](docs/BreakingChangesFrom-v3.0-to-v4.0.md) TODO
- [From v2.1 to v3.0](docs/BreakingChangesFrom-v2.1-to-v3.0.md)
- [From v2.0 to v2.1](docs/BreakingChangesFrom-v2.0-to-v2.1.md)
- [From v1.* to v2.*](docs/BreakingChangesFrom-v1-to-v2.md)
- [Previous notes](docs/OldReleaseNotes.md)

# Release notes
- [v4.0 Release Notes](docs/ReleaseNotes-v4.0.md) TODO
- [v3.0 Release Notes](docs/ReleaseNotes-v3.0.md)
