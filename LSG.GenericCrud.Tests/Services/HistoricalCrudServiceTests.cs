using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bogus;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using LSG.GenericCrud.Tests.Models;
using Moq;
using Xunit;

namespace LSG.GenericCrud.Tests.Services
{
    public class HistoricalCrudServiceTests
    {
        private readonly IQueryable<TestEntity> _entities;
        private readonly TestEntity _entity;
        private readonly IQueryable<HistoricalEvent> _events;
        private readonly IQueryable<HistoricalChangeset> _changesets;
        private readonly HistoricalChangeset _changeset;

        public HistoricalCrudServiceTests()
        {
            Randomizer.Seed = new Random(1234567);
            var entityFaker = new Faker<TestEntity>().
                RuleFor(_ => _.Id, Guid.NewGuid()).
                RuleFor(_ => _.Value, _ => _.Lorem.Word());
            _entities = entityFaker.Generate(5).AsQueryable();
            _entity = entityFaker.Generate();
            var eventFaker = new Faker<HistoricalEvent>().
                RuleFor(_ => _.Id, Guid.NewGuid).
                RuleFor(_ => _.Action, HistoricalActions.Delete.ToString).
                RuleFor(_ => _.CreatedDate, DateTime.Now).
                RuleFor(_ => _.EntityId, _entity.Id.ToString()).
                RuleFor(_ => _.Changeset, new Faker<HistoricalChangeset>()
                    .RuleFor(_ => _.ObjectData, "{}")
                    .RuleFor(_ => _.ObjectDelta, "{}"));
            _events = new List<HistoricalEvent>() { eventFaker.Generate() }.AsQueryable();
            var changesetFaker = new Faker<HistoricalChangeset>()
                .RuleFor(_ => _.Id, Guid.NewGuid)
                .RuleFor(_ => _.EventId, _events.ToArray()[0].Id)
                .RuleFor(_ => _.CreatedDate, DateTime.MinValue)
                .RuleFor(_ => _.ObjectData, "{}")
                .RuleFor(_ => _.ObjectDelta, "{}");
            _changesets = new List<HistoricalChangeset> { changesetFaker.Generate() }.AsQueryable();
            _changeset = changesetFaker.Generate();
        }

        [Fact]
        public void Constructor_SetAutoCommitToFalse()
        {
            var crudServiceMock = new Mock<ICrudService<Guid, TestEntity>>();
            var crudService = crudServiceMock.Object;
            var service = new HistoricalCrudServiceBase<Guid, TestEntity>(crudService, null, null, null, null);

            Assert.False(service.AutoCommit);
            Assert.False(crudService.AutoCommit);
        }

        [Fact]
        public async void RestoreFromChangeset_ThrowsEntityNotFoundException()
        {
            var repository = new Mock<ICrudRepository>();
            var crudService = new Mock<ICrudService<Guid, TestEntity>>();
            var service = new HistoricalCrudServiceBase<Guid, TestEntity>(crudService.Object, repository.Object, null, null, null);

            await Assert.ThrowsAsync<EntityNotFoundException>(() => service.RestoreFromChangeset(_entity.Id, _changeset.Id));
        }

        [Fact]
        public async void RestoreFromChangeset_ThrowsChangesetNotFoundException()
        {
            var repository = new Mock<ICrudRepository>();
            repository.Setup(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>())).ReturnsAsync(_entity);
            var crudService = new Mock<ICrudService<Guid, TestEntity>>();
            var service = new HistoricalCrudServiceBase<Guid, TestEntity>(crudService.Object, repository.Object, null, null, null);

            await Assert.ThrowsAsync<ChangesetNotFoundException>(() => service.RestoreFromChangeset(_entity.Id, _changeset.Id));
        }
        [Fact]
        public async void RestoreFromChangeset_ReturnsOk()
        {
            var repository = new Mock<ICrudRepository>();
            repository.Setup(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>())).ReturnsAsync(_entity);
            repository.Setup(_ => _.GetByIdAsync<Guid, HistoricalChangeset>(It.IsAny<Guid>())).ReturnsAsync(_changeset);
            var crudService = new Mock<ICrudService<Guid, TestEntity>>();
            crudService.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(_entity);
            crudService.Setup(_ => _.UpdateAsync(It.IsAny<Guid>(), It.IsAny<TestEntity>())).ReturnsAsync(_entity);
            var service = new HistoricalCrudServiceBase<Guid, TestEntity>(crudService.Object, repository.Object, null, null, null);

            await service.RestoreFromChangeset(_entity.Id, _changeset.Id);

            crudService.Verify(_ => _.UpdateAsync(It.IsAny<Guid>(), It.IsAny<TestEntity>()), Times.Once);
            repository.Verify(_ => _.CreateAsync<Guid, HistoricalEvent>(It.IsAny<HistoricalEvent>()), Times.Once);
            repository.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async void CopyFromChangeset_ThrowsEntityNotFoundException()
        {
            var repository = new Mock<ICrudRepository>();
            var crudService = new Mock<ICrudService<Guid, TestEntity>>();
            var service = new HistoricalCrudServiceBase<Guid, TestEntity>(crudService.Object, repository.Object, null, null, null);

            await Assert.ThrowsAsync<EntityNotFoundException>(() => service.CopyFromChangeset(_entity.Id, _changeset.Id));
        }

        [Fact]
        public async void CopyFromChangeset_ThrowsChangesetNotFoundException()
        {
            var repository = new Mock<ICrudRepository>();
            repository.Setup(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>())).ReturnsAsync(_entity);
            var crudService = new Mock<ICrudService<Guid, TestEntity>>();
            var service = new HistoricalCrudServiceBase<Guid, TestEntity>(crudService.Object, repository.Object, null, null, null);

            await Assert.ThrowsAsync<ChangesetNotFoundException>(() => service.CopyFromChangeset(_entity.Id, _changeset.Id));
        }
        [Fact]
        public async void CopyFromChangeset_ReturnsOk()
        {
            var repository = new Mock<ICrudRepository>();
            repository.Setup(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>())).ReturnsAsync(_entity);
            repository.Setup(_ => _.GetByIdAsync<Guid, HistoricalChangeset>(It.IsAny<Guid>())).ReturnsAsync(_changeset);
            var crudService = new Mock<ICrudService<Guid, TestEntity>>();
            crudService.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(_entity);
            crudService.Setup(_ => _.CreateAsync(It.IsAny<TestEntity>())).ReturnsAsync(_entity);
            var service = new HistoricalCrudServiceBase<Guid, TestEntity>(crudService.Object, repository.Object, null, null, null);

            await service.CopyFromChangeset(_entity.Id, _changeset.Id);

            crudService.Verify(_ => _.CreateAsync(It.IsAny<TestEntity>()), Times.Once);
            repository.Verify(_ => _.CreateAsync<Guid, HistoricalEvent>(It.IsAny<HistoricalEvent>()), Times.Once);
            repository.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async void Copy_ReturnsCreatedElement()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.CreateAsync<Guid, TestEntity>(It.IsAny<TestEntity>())).ReturnsAsync(_entity);
            var crudServiceMock = new Mock<ICrudService<Guid, TestEntity>>();
            crudServiceMock.Setup(_ => _.CopyAsync(It.IsAny<Guid>())).ReturnsAsync(new TestEntity());
            var crudService = crudServiceMock.Object;
            var service = new HistoricalCrudServiceBase<Guid, TestEntity>(crudService, repository.Object, null, null, null);

            var result = await service.CopyAsync(_entity.Id);

            Assert.NotEqual(_entity.Id, result.Id);
            repository.Verify(_ => _.CreateAsync<Guid, HistoricalEvent>(It.IsAny<HistoricalEvent>()), Times.Once);
            crudServiceMock.Verify(_ => _.CopyAsync(It.IsAny<Guid>()), Times.Once);
            repository.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async void Delta_DeltaRequestNotProvided_Throws_ArgumentNullException()
        {
            var crudService = new Mock<ICrudService<Guid, TestEntity>>().Object;
            var service = new HistoricalCrudServiceBase<Guid, TestEntity>(crudService, null, null, null, null);

            await Assert.ThrowsAsync<ArgumentNullException>(() => service.Delta(_entity.Id, null));
        }

        //TODO: Complete this unit tests
        //[Fact]
        //public async void Delta_DeltaRequestProvided_CallsGetLastTimeViewed()
        //{
        //    var lastTimeView = DateTime.MinValue;
        //    var readeableService = new Mock<IHistoricalCrudReadService<Guid, TestEntity>>();
        //    var repository = new Mock<ICrudRepository>();

        //    readeableService.Setup(_ => _.GetLastTimeViewed<TestEntity>(It.IsAny<Guid>())).Returns(lastTimeView);
        //    var service = new HistoricalCrudServiceBase<Guid, TestEntity>(
        //        new Mock<ICrudService<Guid, TestEntity>>().Object, 
        //        repository.Object, 
        //        null, 
        //        null,
        //        null);

        //    await Assert.ThrowsAsync<NullReferenceException>(() => service.Delta(_entity.Id, new DeltaRequest()));

        //    readeableService.Verify(_ => _.GetLastTimeViewed<TestEntity>(_entity.Id), Times.Once);
        //}

        [Fact]
        public async void Delta_DeltaRequestProvided_DoNotCallsGetLastTimeViewed()
        {
            var lastTimeView = DateTime.MinValue;
            var readeableService = new Mock<IHistoricalCrudReadService<Guid, TestEntity>>();
            readeableService.Setup(_ => _.GetLastTimeViewed<TestEntity>(It.IsAny<Guid>())).Returns(lastTimeView);
            var service = new HistoricalCrudServiceBase<Guid, TestEntity>(
                new Mock<ICrudService<Guid, TestEntity>>().Object,
                null,
                null,
                null,
                null);

            await Assert.ThrowsAsync<NullReferenceException>(() => service.Delta(_entity.Id, new DeltaRequest() { From= DateTime.Now }));

            readeableService.Verify(_ => _.GetLastTimeViewed<TestEntity>(_entity.Id), Times.Never);
        }

        [Fact]
        public async void GetDeltaSnapshot_NoHistory_Throws_NoHistoryException()
        {
            var repository = new Mock<ICrudRepository>();
            repository.Setup(_ => _.GetAll<Guid, HistoricalEvent>()).Returns(_events);
            var crudService = new Mock<ICrudService<Guid, TestEntity>>();
            var service = new HistoricalCrudServiceBase<Guid, TestEntity>(crudService.Object, repository.Object, null, null, null);

           await Assert.ThrowsAsync<NoHistoryException>(() => service.GetDeltaSnapshot(_entity.Id, DateTime.MinValue, DateTime.MaxValue));
        }
        
        // TODO Complete this test
        //[Fact]
        //public async void GetDeltaSnapshot_Ok()
        //{
        //    var repository = new Mock<ICrudRepository>();
        //    repository.Setup(_ => _.GetAllAsync<Guid, HistoricalEvent>()).ReturnsAsync(_events);
        //    repository.Setup(_ => _.GetAllAsync<Guid, HistoricalChangeset>()).ReturnsAsync(_changesets);
        //    repository.Setup(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>())).ReturnsAsync(_entity);
        //    var crudService = new Mock<ICrudService<Guid, TestEntity>>();
        //    var service = new HistoricalCrudServiceBase<Guid, TestEntity>(crudService.Object, repository.Object, null, null, null);

        //    var result = await service.GetDeltaSnapshot(_entity.Id, DateTime.MinValue, DateTime.MaxValue);   
        //}
        // TODO add test for getlasttimeviewed


    }
}
