# LSG.GenericCrud

# Introduction
This library is used to provide simplified RESTful CRUD apis with:
- Untyped Crud Controller
- Untyped Repository. 

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
- Automatic DTO to Entity (and Entity to DTO) mapping for Crud\<T> or HistoricalCrud\<T> controllers: [More details](./docs/FeatureDTO.md)
- Automatic data fillers: [More details](./docs/FeatureDataFillers.md)
- Support for custom repository logic: *more details to come*

Actually unsupported features (but I'm working on it!):
- Per entity security policies
- Full async pipeline
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

# Release Notes

- v1.1.0: Support/Compatibility for .NET Core 2.0 and .NET Standard 2.0
- v1.0.1: Adding support for interfacable repositories
- v1.0.0: Initial version
