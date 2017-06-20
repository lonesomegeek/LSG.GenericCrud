# lonesomegeek - Generic Crud
This library is providing a set of tools to facilitate CRUD scenarios with .NET applications. In a day-to-day basis, we are creating a lot of new code to support simple CRUD operation on entities. This tools remove the boilerplate code you need to create and let you maximise your time designing meaningful business logic.

## Getting started
To get started with this library, you just need to add a Nuget packge to your existing .NET application (LSG.GenericCrud) 

```
Install-Package LSG.GenericCrud
```

OR 

do it the hard way (aka checkout and build the code yourself)!

## Features
This library is designed with two layers:

1. Controllers: Where the routing logic goes
2. Repositories: Where the data transaction occurs with the database

Note: The database supported for now must be compatible with EntityFrameworkCore.

This library support two CRUD scenarios:

1. Simple CRUD 
2. Historical CRUD, each transaction will be logged

## Samples
To get more information on samples providing different CRUD scenarios, take a look at this documentation.

If you want to have a working aspnetcore RESTful sample API, take a look here.

## Feedback & support
Feedback are welcomed as I will be able to plan next iteration of this library.

Enjoy!