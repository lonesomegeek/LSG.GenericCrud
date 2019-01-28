# Breaking changes
## Dependencies
- Switch from netcore 2.0 to 2.2
- Switch from aspnetcore 2.0 to 2.2
## Code changes that will be needed
- If you did A
    - This change is needed
    - Reason: ...

doc:
- remove async samples, everything is async now
- remove doc for async feature?
TODOs
- How to start with VS and VSCode + Command line, not clear

# Reflexion around v3.* version
## TL;DR Version
For sake of flexibility, I've made a new version mainly around composition over an inheritance model (v2.* and prior versions). See breaking changes section to do the upgrade if you need it.

## Complete story

What made me change my mind and create a new version of my library is pretty simple. I was talking with my teammates a few weeks ago about many ways of enabling more functionalities around CRUD. I was telling them that it could be great for me (or another user) to know which records in a DB have changed since my last time in the application (like a mail reader).

I've made a pretty simple plan during the holiday. Three tables, new routes, new feature. But, I've come to a dead end trying to wire this thing up with my existing code base.

Let's pretend:
- CrudController is A
- HistoricalController is B
- ReadeableController is C (my new and awesome feature I'm trying to release)

Let's also pretend that:
- If with entity of type X, I only need A
- If with entity of type Y, I need A and B
- If with entity of type Z, I need A and C
- ...

Things are getting pretty complicated with an inheritance model... That much complicated that it needs a core refactoring.

All layers have been refactored to switch to a composition model in which, dependencies are implemented and forwarded to base logic. Let me explain things, let's say you have an entity of type Z and you need feature A and C. Your controller will need to:
- implement controller base features of A and C
- forward requests to controller injected classes

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

        // ... implementation code: see samples for more details
    }
```
Hope it helped understand why these changes were needed!
