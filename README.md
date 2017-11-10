# LSG.GenericCrud

# Introduction
This library is used to provide simplified RESTful CRUD apis with a multilayer architecture:
- Generic Crud Controller
- Generic Entity Framework Repository. 

This library allies injection (IoC), minimum codebase for maximum efficiency.

Obviously, when you are working with databases and entities, you need to write some code to be able to manage these entities. And, when working with multiple kind of entities, you are **forced** to rewrite code that is similar for each  entities. This library is getting rid of the duplicated code and keeps everything **DRY**.

Enjoy!

# Prerequisites
You need:
- [.NET Core 2.0 SDK](https://github.com/dotnet/core/blob/master/release-notes/download-archives/2.0.0-download.md) (for any of the options below)

You also need one of these options below:
- [Visual Studio](https://www.visualstudio.com/downloads/), at least a version that supports aspnetcore (Visual Studio 2015 Update 3+)
- [Visual Studio Code](https://code.visualstudio.com/)
- A command line and some will-power =P

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

# Features supported in this library

I've designed this library to be pretty extensible. Here is some features supported and links to further documentation:
- Support for entity history tracking: [More details](./docs/FeatureHistoricalCrud.md)
- Automatic DTO to Entity (and Entity to DTO) mapping for Crud\<T> or HistoricalCrud\<T> controllers, provided by [LSG.GenericCrud.Dto](https://www.nuget.org/packages/LSG.GenericCrud.Dto/): [More details](./docs/FeatureDTO.md)
- Automatic data fillers: [More details](./docs/FeatureDataFillers.md)
- Full async pipeline: [More details](./docs/FeatureAsync.md)
- Support for custom repository logic: *more details to come*

Actually unsupported features (feel free to help if you want!):
- Per entity security policies
- *and more*...

# Getting started / Tutorials

I am presenting here really simple scenarios, more complex will come:
- RESTFul CRUD API for *Account* Entity 
    - Using Visual Studio Code (or command line): [Tutorial](docs/1_TutorialAcocuntCrudVisualStudioCode.md)
    - Using Visual Studio (2015 update 3+): [Tutorial](docs/1_TutorialAcocuntCrudVisualStudio.md)

Want to have a look to more samples, take a look at the *samples* or clone it for more embedded fun =P: [Link](https://github.com/lonesomegeek/LSG.GenericCrud.Samples)

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
- [From v1.* to v2.*](docs/BreakingChangesFrom-v1-to-v2.md)

# Old Release Notes

- v1.1.2:
    - Bugfix: 
        - [#28](https://github.com/lonesomegeek/LSG.GenericCrud/issues/28)
        - [#30](https://github.com/lonesomegeek/LSG.GenericCrud/issues/30)
- v1.1.1:
  - Bugfix [#29](https://github.com/lonesomegeek/LSG.GenericCrud/issues/29)
- v1.1.0: Support/Compatibility for .NET Core 2.0 and .NET Standard 2.0
- v1.0.1: Adding support for interfacable repositories
- v1.0.0: Initial version
