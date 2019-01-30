# LSG.GenericCrud
[![NuGet](https://img.shields.io/nuget/dt/LSG.GenericCrud.svg)](https://www.nuget.org/packages/LSG.GenericCrud)
[![NuGet](https://img.shields.io/nuget/v/LSG.GenericCrud.svg)](https://www.nuget.org/packages/LSG.GenericCrud)
[![GitHub issues](https://img.shields.io/github/issues/lonesomegeek/LSG.GenericCrud.svg)](https://github.com/lonesomegeek/LSG.GenericCrud/issues)
[![GitHub license](https://img.shields.io/github/license/lonesomegeek/LSG.GenericCrud.svg)](https://github.com/lonesomegeek/LSG.GenericCrud/blob/master/LICENSE)

# Introduction
This library is used to provide simplified RESTful CRUD apis with a multilayer architecture:
- Generic CRUD Controller layer
- Generic CRUD Service layer
- Generic CRUD Repository layer 

This library allies injection (IoC), minimum codebase for maximum efficiency.

Obviously, when you are working with databases and entities, you need to write some code to be able to manage these entities. And, when working with multiple kind of entities, you need to rewrite code that is similar for each entities. This library is getting rid of the duplicated code and keeps everything **DRY**.

Enjoy!

# Prerequisites
You need:
- [.NET Core 2.2 SDK](https://dotnet.microsoft.com/download/dotnet-core/2.2) (for any of the options below)

You also need one of these options below:
- [Visual Studio](https://www.visualstudio.com/downloads/), at least a version that supports aspnetcore 2.2 (Visual Studio 2017 Update 9+ - v15.9+)
- [Visual Studio Code](https://code.visualstudio.com/)
- For superheroes: A command line, notepad, and some will-power =P

# What is CRUD?

CRUD stands for Create, Read, Update, Delete

See [wikipedia](https://en.wikipedia.org/wiki/Create,_read,_update_and_delete) definition.

CRUD is not everything. You can do CRUD in many ways. But, in these days, we (as developers) are more in the JSON and in the REST things... This is what this library is for! Doing RESTful CRUD operations in a simplified way!

Here is a sample of RESTful CRUD URLs for an entity of type Account:

| VERB   | URL               | Description           |
|--------|-------------------|-----------------------|
| GET    | /api/accounts     | Retreive all accounts |
| GET    | /api/accounts/:id | Retreive one account  |
| POST   | /api/accounts     | Create one account    |
| PUT    | /api/accounts/:id | Update one account    |
| DELETE | /api/accounts/:id | Delete one account    |

# Features supported, by library

I've designed this library to be pretty extensible. Here is some features supported and links to further documentation:

- [LSG.GenericCrud](./TODO)
    - Support for entity history tracking: [More details](./docs/FeatureHistoricalCrud.md)
    - Data fillers: [More details](./docs/FeatureDataFillers.md)
- [LSG.GenericCrud.Dto](./TODO)
    - DTO to Entity (and Entity to DTO) mapping for Crud\<T> or HistoricalCrud\<T> controllers: [More details]
- [LSG.GenericCrud.Extensions](./TODO)
    - Readeable CRUD, to let you know personaly if something has changed since last view: [More details](./docs/FeatureReadeableCrud)
    - More middlewares...

> Note that all libraries supports customized layer for each layer: *more details to come*

Actually unsupported features (feel free to help if you want!):
- Per entity security policies
- *and more*...

# Getting started / Tutorials

Simpliest scenarios:
- Using Visual Studio Code (or command line): [Tutorial](docs/1_TutorialAcocuntCrudVisualStudioCode.md)
- Using Visual Studio (2017 update 9+ - v15.9+): [Tutorial](docs/1_TutorialAcocuntCrudVisualStudio.md)

Before diving into more complex scenarios, you should take a rapid view at [this explaniation of the new architecture in v3.*](./docs/v3.0-new-architecture.md).

> Want to have a look to *more samples*, take a look at these *samples*: [Link](LSG.GenericCrud.Samples/README.md)

# Install this library

If you already know what to do to get up'n'running with this library, use one of these commands to install my [NugetPackage](https://www.nuget.org/packages/LSG.GenericCrud/):

If using dotnet cli in an existing project 
```bash
dotnet add package LSG.GenericCrud
```

If using Package Manager Console (in Visual Studio)
```bash
Install-Package LSG.GenericCrud
```

## More super powers!
If you need other features that are not in the base library, you can install these libraries:
- Dto/Entity mapping support: [LSG.GenericCrud.Dto](https://www.nuget.org/packages/LSG.GenericCrud.Dto/)
- Data Fillers and middlewares: [LSG.GenericCrud.Extensions](https://www.nuget.org/packages/LSG.GenericCrud.Extensions)

Note: There is actually missing documentation on what is included in these libraries. More documentation will come!

# Breaking changes
- [From v2.1 to v3.0](docs/BreakingChangesFrom-v2.1-to-v3.0.md)
- [From v2.0 to v2.1](docs/BreakingChangesFrom-v2.0-to-v2.1.md)
- [From v1.* to v2.*](docs/BreakingChangesFrom-v1-to-v2.md)
- [Previous notes](docs/OldReleaseNotes.md)

# Release notes
- [v3.0 Release Notes](docs/ReleaseNotes-v3.0.md)