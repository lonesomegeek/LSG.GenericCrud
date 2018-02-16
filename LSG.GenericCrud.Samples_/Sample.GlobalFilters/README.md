# How to launch this sample
1. Open this sample in Visual Studio
2. Press F5 to get up'n'running

# Routes available in this sample

| VERB   | URL                         | Description                           |
|--------|-----------------------------|---------------------------------------|
| GET    | /api/cars                   | Retreive all cars (in your tenant)    |
| GET    | /api/cars/:id               | Retreive one car (in your tenant)     |
| POST   | /api/cars                   | Create one car (in your tenant)       |
| PUT    | /api/cars/:id               | Update one car (in your tenant)       |
| DELETE | /api/cars/:id               | Delete one car (in your tenant)       |
| GET    | /api/cars/all               | Get all cars (bypass tenant filter)   |
| GET    | /api/items                  | Retreive all items (in your tenant)   |
| GET    | /api/items/:id              | Retreive one item (in your tenant)    |
| POST   | /api/items                  | Create one item (in your tenant)      |
| PUT    | /api/items/:id              | Update one item (in your tenant)      |
| DELETE | /api/items/:id              | Delete one item (in your tenant)      |
| GET    | /api/items/all              | Get all items (bypass tenant filter)  |
| GET    | /api/configuration/TenantId | Retreive your tenant id configuration |
| POST   | /api/configuration/TenantId | Set your tenant id configuration      |

[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/860c2303c6732f73793d)

# Explanation

## Introduction

This sample is more complex. It will show the power of the LSG.GenericCrud library in conjunction with EFCore and ASPNETCore showing what we can do with Entity Framework GlobalFilters. If you want to have more information on what is Global Filters, follow [this link](https://docs.microsoft.com/en-us/ef/core/querying/filters).

This sample will demonstrate these scenarios:
- Software delete demonstrated with the Item entity
- Entity data tied to a tenant (Multi tenancy) demonstrated with the Car entity

## Software delete
A software delete is the action of identifying an entity that it is deleted but not really deleted (suppressed) from disk/db/table/... To acheive that, I've done a few things in the sample to enable software delete:
- Create a ISoftwareDelete interface 
- Create a software delete datafiller to fill information about the deleted flag
- Adjust the Entity Framework database context to tell it to use a Global Filter with my entity

### ISoftwareDelete interface
To track the software delete feature, all entities that I need to be "software deleteable" needs to implement the ISoftwareDelete interface.

Code files:
- [Models/ISoftwareDelete.cs](Sample.GlobalFilters/Models/ISoftwareDelete.cs)

### DataFiller
When the database context will receive an instruction, it will ask the SoftwareDeleteDataFiller to check if there is an action required there. If the entity is implementing ISoftwareDelete and is of deleted state, there is need for an action.

Code files:
- [DataFillers/SoftwareDeleteDataFiller.cs](Sample.GlobalFilters/DataFillers/SoftwareDeleteDataFiller.cs)

### EntityFramework database context with Global Filters
Entity Framework needs to know on a "per entity type" what filter to apply. What we want is to change a:

```sql
SELECT * FROM dbo.Cars
``` 

to

```sql
SELECT * FROM dbo.Cars Where IsDeleted == false
``` 

Code files:
- [Models/SampleContext.cs](Sample.GlobalFilters/Models/SampleContext.cs)

## Entity data tied to a tenant (Multi tenancy)
For those of you who do not know what is a tenant, let's make things simple. When you are using an application (let's say Facebook), Facebook is showing you the data regarding you account. We can say that the information shown to you is in your tenant. You can also hear about multitenancy in SaaS appplications (Software as a Service).

The sample will demonstrate how to isolate entities with a multitenancy approach. To acheive that, I've done a few thnigs in the sample to enable multitenancy:
- Create a ISingleTenantEntity
- Create a tenant datafiller to fill information about the user's tenant
- Create a configuration controller to enable (for test purposes only) to manage user's tenant id
- Adjust the Entity Framework database context to tell it to user a Global Filter with my entity

### ISingleTenantEntity interface
To track the tenant information, all entities that I need to be "tenant ready" needs to implement the ISingleTenantEntity interface.

Code files:
- [Models/ISingleTenantEntity.cs](Sample.GlobalFilters/Models/ISingleTenantEntity.cs)

### DataFiller
When the database context will receive an instruction, it will ask the SingleTenantDataFiller to check if there is an action required there. If the entity is implementing ISingleTenantEntity and is of created state, there is need for an action.

Code files:
- [DataFillers/SingleTenantDataFiller.cs](Sample.GlobalFilters/DataFillers/SingleTenantDataFiller.cs)

### Configuration controller
This controller will enable (for testing purposes only) the modification of the TenantId key to be able to test different tenant ids.

Code files:
- [Controllers/ConfigurationController.cs](Sample.GlobalFilters/Controllers/ConfigurationController.cs)

### EntityFramework database context with Global Filters
Entity Framework needs to know on a "per entity type" what filter to apply. What we want is to change a:

```sql
SELECT * FROM dbo.Items
``` 

to

```sql
SELECT * FROM dbo.Items Where TenantId == "1234..."
``` 

Code files:
- [Models/SampleContext.cs](Sample.GlobalFilters/Models/SampleContext.cs)

Related links:
- Multitenancy: http://whatis.techtarget.com/definition/multi-tenancy
- SaaS: http://whatis.techtarget.com/definition/SaaS
