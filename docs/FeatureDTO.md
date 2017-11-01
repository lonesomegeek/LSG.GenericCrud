# Feature: DTO Pattern
When working with business entities, the first big error that you will make is suppose that this entity is the truth and will be like that forever. You are making a big mistake here.

Let's suppose you are working with the **account** entity. 
- Day 1, the entity might look like X. 
- Day 2, you are acquiring a new businnes using a CRM and your entity might look like X+Y.
- Dat 3, .... your entity might look like GODZILLA with the perfermance issues related to it...

First thing to do, is abstracting the business entity tighted to technologies (ie.: CRM for an Account for exemple) and generalize it to be able to have a simple, portable and flexible DTO! But you are asking, what the heck is a DTO. DTO is a design pattern/principle. It stands for Data Transfer Object. It is used in development process to create a new object that will be used in data transfer to abstract the business objects. And that methodology:
- will survive new software migration that may modify your entity
- will survive the aggregation of many systems to create your entity
- will obfuscate systems
- ...

I'm not a master in technical explanation of DTO, but it is really working. And my library is supporting it natively, so ... USE IT :)
You will need to import this nuget package to get DTO feature working: [LSG.GenericCrud.Dto](https://www.nuget.org/packages/LSG.GenericCrud.Dto/)

## What is happening behind the scenes
Before getting into further details, I think this is important for you to understand what is happening behind the scenes.

First of all, you have an entity (further called Business Object @ BO) and a DTO related to it. A **BO** is a class representing the business data. A **DTO** is representing the data that will be transfered to consumer.

To be able to *map* a **BO** and a **DTO**, you will need a *Mapper*. You can do this all by hand in a service layer for example. My library is doing it four you with simple configurations with the big help of [AutoMapper](https://github.com/AutoMapper/AutoMapper). This library come in handy to facilitate the mapping process.

When you have:
- A **BO** class
- A **DTO** class
- A mapping that is *mapping* **BO** & **DTO**

You are done ! :)

## How to make it "DTO Style"
To enable the *DTO* feature, you have few steps to do to make the thing work:
- Create the entity class
- Create the dto class
- Configure the mapping between the two
- Adjust your controller to tell it to manage dto mapping
### Create the entity class
Create a class named AccountModel.cs:

```csharp
public class AccountModel : IEntity
{
    public AccountModel()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int AnnualRevenue { get;set; }
}
```

### Create the dto class
Create a class named AccountDto.cs:

```csharp
public class AccountDto : IEntity
{
    public AccountDto()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public string FullName { get; set; }
}
```

### Data mapping
Adjust your **Startup.cs** to add data mapping in **ConfigureServices(...)** method:
```csharp
var automapperConfiguration = new AutoMapper.MapperConfiguration(_ =>
{
    _.CreateMap<AccountDto, AccountModel>()
        .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FullName.Split(',', StringSplitOptions.None)[0]))
        .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.FullName.Split(',', StringSplitOptions.None)[1]));
    _.CreateMap<AccountModel, AccountDto>()
        .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => $"{src.FirstName},{src.LastName}"));
});
services.AddSingleton(automapperConfiguration.CreateMapper());
```

**Note:** You did two things with your DTO:
- Data masking: You hid the AnnualRevenue property from the business definition
- Data aggregation: You aggregate *FirstName* and *LastName* to create a new field called *FullName*

### Controller adjustment
Here is a CrudController\<T> (CrudController\<TEntity>)
```csharp
[Route("api/[controller]")]
public class AccountsController : CrudController<AccountModel>
{
    public AccountsController(ICrudService<AccountModel> service) : base(service)
}
```

Here is a controller ready for DTO mapping (CrudController\<TDto, TEntity>)
```csharp
[Route("api/[controller]")]
public class AccountsController : CrudController<AccountDto>
{
    public AccountsControllerCrudService<AccountDto, AccountModel> service) : base(service)
}
```

The differences are:
- The class inherits from a different type: a CrudController able to manage TDto, TEntity mapping
- A second parameter is needed in the default constructor, this paramter is used to pass an injected auto mapper definition to the execution flow.

## Samples

Here is a link to the DTO mapping source code sample: [Link](https://github.com/lonesomegeek/LSG.GenericCrud.Samples/tree/master/Sample.Dto)
