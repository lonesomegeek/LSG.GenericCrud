using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Extensions.Controllers;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SampleController :
        ControllerBase,
        ICrudController<Account>,
        IReadeableCrudController<Account>
    {
        private readonly IHistoricalCrudController<Account> _crudController;
        private readonly IReadeableCrudController<Account> _readeableCrudController;
        private readonly ICrudRepository _repository;

        public SampleController(
            IHistoricalCrudController<Account> crudController, 
            IReadeableCrudController<Account> readeableCrudController,
            ICrudRepository repository)
        {
            _crudController = crudController;
            _readeableCrudController = readeableCrudController;
            _repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<Account>> Create([FromBody] Account entity) => await _crudController.Create(entity);
        [HttpDelete("{id}")]
        public async Task<ActionResult<Account>> Delete(Guid id) => await _crudController.Delete(id);
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAll() => await _crudController.GetAll();
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<Account>> GetById(Guid id) => await _crudController.GetById(id);
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Account entity) => await _crudController.Update(id, entity);
        [HttpPost("{id}/restore")]
        public async Task<IActionResult> Restore(Guid id) => await _crudController.Restore(id);
        [HttpGet("{id}/history")]
        public virtual async Task<IActionResult> GetHistory(Guid id) => await _crudController.GetHistory(id);

        [HttpGet("readstatus")]
        public async Task<IActionResult> GetAllReadStatus() => await _readeableCrudController.GetAllReadStatus();
        [HttpPost]
        [Route("read")]
        public async Task<IActionResult> MarkAllAsRead() => await _readeableCrudController.MarkAllAsRead();
        [HttpPost]
        [Route("unread")]
        public async Task<IActionResult> MarkAllAsUnread() => await _readeableCrudController.MarkAllAsUnread();
        [HttpPost]
        [Route("{id}/read")]
        public async Task<IActionResult> MarkOneAsRead(Guid id) => await _readeableCrudController.MarkOneAsRead(id);
        [HttpPost]
        [Route("{id}/unread")]
        public async Task<IActionResult> MarkOneAsUnread(Guid id) => await _readeableCrudController.MarkOneAsUnread(id);

        [HttpGet("{id}/deltasnapshot")]
        public async Task<IActionResult> GetDeltaSnapshot(Guid id)
        {
            var events = _repository
                .GetAll<HistoricalEvent>()
                .Where(_ => _.EntityId == id)
                .OrderBy(_ => _.CreatedDate);
            // snapshot from creation date
            var sourceEvent = events
                .FirstOrDefault();
            var sourceObject = JsonConvert.DeserializeObject<Account>(sourceEvent.Changeset);
            var actual = _repository.GetById<Account>(id);
            var changeset = actual.DetailedCompare(sourceObject);

            var snapshotChangeset = new SnapshotChangeset();
            snapshotChangeset.EntityTypeName = sourceEvent.EntityName;
            snapshotChangeset.EntityId = sourceEvent.EntityId;
            snapshotChangeset.LastViewed = DateTime.Now;
            snapshotChangeset.LastModifiedBy = events.Last().CreatedBy;
            snapshotChangeset.LastModifiedDate = events.Last().CreatedDate.Value;
            snapshotChangeset.Changes = new List<Change>();

            actual
                .GetType()
                .GetProperties()
                .Where(_ => _.DeclaringType == actual.GetType())
                .ToList()
                .ForEach(_ => snapshotChangeset.Changes.Add(new Change()
                {
                    FieldName = _.Name,
                    FromValue = sourceObject.GetType().GetProperty(_.Name).GetValue(sourceObject),
                    ToValue = actual.GetType().GetProperty(_.Name).GetValue(actual)
                }));
            
            return Ok(snapshotChangeset);
        }

        [HttpGet("{id}/deltadifferential")]
        public async Task<IActionResult> GetDeltaDifferential(Guid id)
        {
            // snapshot from creation date
            var events = _repository
                .GetAll<HistoricalEvent>()
                .Where(_ => _.EntityId == id)
                .OrderBy(_ => _.CreatedDate);
            var sourceEvent = events.First();
            var sourceObject = JsonConvert.DeserializeObject<Account>(sourceEvent.Changeset);
            var differentialChangeset = new DifferentialChangeset();
            differentialChangeset.EntityId = id;
            differentialChangeset.EntityTypeName = sourceEvent.EntityName;
            differentialChangeset.LastViewed = events.Last().CreatedDate.Value;
            differentialChangeset.Changesets = new List<Changeset>();

            var currentEvent = sourceEvent;
            var currentObject = sourceObject;
            for (int i = 1; i < events.Count(); i++)
            {
                var nextEvent = events.ToArray()[i];
                var nextEventObject = JsonConvert.DeserializeObject<Account>(currentEvent.Changeset);
                var changeset = new Changeset();
                changeset.Date = nextEvent.CreatedDate.Value;
                changeset.UserId = nextEvent.CreatedBy;
                changeset.Changes = new List<Change>();
                sourceObject
                    .GetType()
                    .GetProperties()
                    .Where(_ => _.DeclaringType == sourceObject.GetType())
                    .ToList()
                    .ForEach(_ => changeset.Changes.Add(new Change()
                    {
                        FieldName = _.Name,
                        FromValue = currentObject.GetType().GetProperty(_.Name).GetValue(currentObject),
                        ToValue = nextEventObject.GetType().GetProperty(_.Name).GetValue(nextEventObject)
                    }));
                differentialChangeset.Changesets.Add(changeset);

                currentObject = nextEventObject;
                currentEvent = nextEvent;
            }
            // add last event to current object
            var lastChangeset = new Changeset();
            var lastObject = _repository.GetById<Account>(id);

            lastChangeset.Date = lastObject.CreatedDate.Value;
            lastChangeset.UserId = lastObject.CreatedBy;
            lastChangeset.Changes = new List<Change>();
            lastObject
                .GetType()
                    .GetProperties()
                    .Where(_ => _.DeclaringType == sourceObject.GetType())
                    .ToList()
                    .ForEach(_ => lastChangeset.Changes.Add(new Change()
                    {
                        FieldName = _.Name,
                        FromValue = currentObject.GetType().GetProperty(_.Name).GetValue(currentObject),
                        ToValue = lastObject.GetType().GetProperty(_.Name).GetValue(lastObject)
                    }));
            differentialChangeset.Changesets.Add(lastChangeset);

            return Ok(differentialChangeset);
        }
    }

    internal class DifferentialChangeset
    {
        public DifferentialChangeset()
        {
        }

        public string EntityTypeName { get; internal set; }
        public Guid EntityId { get; internal set; }
        public DateTime LastViewed { get; internal set; }

        public List<Changeset> Changesets { get; set; }
    }
    internal class Changeset
    {
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public List<Change> Changes { get; set; }
    }
    internal class Change
    {
        public string FieldName { get; set; }
        public object FromValue { get; set; }
        public object ToValue { get; set; }
    }

    internal class SnapshotChangeset
    {
        public SnapshotChangeset()
        {
        }

        public string EntityTypeName { get; internal set; }
        public Guid EntityId { get; internal set; }
        public DateTime LastViewed { get; internal set; }
        public List<Change> Changes { get; internal set; }
        public DateTime LastModifiedDate { get; internal set; }
        public string LastModifiedBy { get; internal set; }
    }
}
