# Breaking changes from v2.1 to v3.*
## Dependencies
- Switch from netcore 2.0 to 2.2
- Switch from aspnetcore 2.0 to 2.2
## Code changes that will be needed
- If you already used CrudAsync* (async versions)
    - Use Crud* instead
    - Reason: I've removed everything that is not async (CrudController, CrudService, ...) and I've put the async versions in the *old* sync versions.
- If you created a controller that inherits from CrudController
    - Nothing to do, supported for now in 3.*
- If you created a controller that inherits from HistoricalCrudController
    - You will have to composition model instead of direct inheritance
    - Reason: Switching from inheritance to composition pattern

# Reflexion around v3.* version
## TL;DR; Version
For sake of flexibility, I've made a new version mainly around composition over an inheritance model instead of inheritance only (v2.* and prior versions). See breaking changes section to do the upgrade if you need it.

## Complete story
What made me change my mind and create a new version of my library is pretty simple. I was talking with my teammates before holidays about many ways of enabling more functionalities around CRUD. I was telling them that, for example, it could be great for me (or another user) to know which records in a DB have changed since my last visit in an application (like a mail reader, but for any kind of entities, in a simple manner).

I've made a pretty simple plan during the holidays. Two tables, new routes, new feature. But, I've come to a dead end trying to wire this thing up with my existing code base (LSG.GenericCrud v2.1).

To explain the dead end, let's pretend that:
- CrudController is A
- HistoricalController is B
- ReadeableController is C (my new and awesome feature I'm trying to release)

Let's also pretend that:
- If with entity of type X, I only need A
- If with entity of type Y, I need A and B
- If with entity of type Z, I need A and C
- If with ... (I think you understand the picture!)

Things can get pretty complicated with an inheritance model when managing all kind of feature matching... That much complicated that it needs a core refactoring.

From the beginning of v3.0, all layers have been refactored to switch to a composition model in which, dependencies are implemented and forwarded to base logic. Let me explain things in a different way. Let's say you have an entity of type Z and you need feature A and C. Your controller (of type Z) will need to:
- inherit from ControllerBase (needed for implementing a netcore REST Api)
- implement interface-of-feature-A
- implement interface-of-feature-C
- forward requests from controller class to implementation classes

My reflections are based on these simple oriented object programming patterns:
- [Composition over inheritance](https://en.wikipedia.org/wiki/Composition_over_inheritance)
- [Forwading object oriented programming](https://en.wikipedia.org/wiki/Forwarding_(object-oriented_programming))

Sample:
```csharp
    public class SampleController :
        ControllerBase,
        ICrudController<Sample>, // implementing feature A
        IReadeableCrudController<Sample> // implementing feature C
    {
        private readonly ICrudController<Sample> _crudController;
        private readonly IReadeableCrudController<Sample> _readeableCrudController;

        public SampleController(
            ICrudController<Sample> crudController, // injective feature A
            IReadeableCrudController<Sample> readeableCrudController) // injecting feature C
        {
            _crudController = crudController;
            _readeableCrudController = readeableCrudController;
        }

        // one forwading sample implemented from ICrudController<Sample>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sample>>> GetAll() => await _crudController.GetAll();

        // ... implementation code: see samples for more details
    }
```
Hope it helped understand why these changes were needed!
Have fun CRUDing ;)!