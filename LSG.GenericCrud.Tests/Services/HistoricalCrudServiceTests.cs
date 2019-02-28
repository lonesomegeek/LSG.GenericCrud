using System;
using System.Collections.Generic;
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
        private readonly IList<TestEntity> _entities;
        private readonly TestEntity _entity;
        private readonly List<HistoricalEvent> _events;

        public HistoricalCrudServiceTests()
        {
            Randomizer.Seed = new Random(1234567);
            var entityFaker = new Faker<TestEntity>().
                RuleFor(_ => _.Id, Guid.NewGuid()).
                RuleFor(_ => _.Value, _ => _.Lorem.Word());
            _entities = entityFaker.Generate(5);
            _entity = entityFaker.Generate();
            var eventFaker = new Faker<HistoricalEvent>().
                RuleFor(_ => _.Id, Guid.NewGuid).
                RuleFor(_ => _.Action, HistoricalActions.Delete.ToString).
                RuleFor(_ => _.EntityId, _entity.Id.ToString()).
                RuleFor(_ => _.Changeset, new Faker<HistoricalChangeset>()
                    .RuleFor(_ => _.ObjectData, "{}"));
            _events = new List<HistoricalEvent>() { eventFaker.Generate() };
        }

        [Fact]
        public void Constructor_SetAutoCommitToFalse()
        {
            var crudServiceMock = new Mock<ICrudService<TestEntity>>();
            var crudService = crudServiceMock.Object;
            var service = new HistoricalCrudService<Guid, TestEntity>(crudService, null, null);

            Assert.False(service.AutoCommit);
            Assert.False(crudService.AutoCommit);
        }

        [Fact]
        public void Create_ReturnsCreatedElement()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.CreateAsync(It.IsAny<TestEntity>())).ReturnsAsync(_entity);
            var crudServiceMock = new Mock<ICrudService<TestEntity>>();
            crudServiceMock.Setup(_ => _.CreateAsync(It.IsAny<TestEntity>())).ReturnsAsync(_entity);
            var crudService = crudServiceMock.Object;
            var service = new HistoricalCrudService<Guid, TestEntity>(crudService, repository.Object, null);

            var result = service.Create(_entity);

            Assert.Equal(_entity.Id, result.Id);
            repository.Verify(_ => _.CreateAsync<Guid, HistoricalEvent>(It.IsAny<HistoricalEvent>()), Times.Once);
            crudServiceMock.Verify(_ => _.CreateAsync(It.IsAny<TestEntity>()), Times.Once);
            repository.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public void Update_ReturnsUpdatedElement()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetByIdAsync<TestEntity>(It.IsAny<Guid>())).ReturnsAsync(_entity);
            var crudServiceMock = new Mock<ICrudService<TestEntity>>();
            crudServiceMock.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(_entity);
            crudServiceMock.Setup(_ => _.UpdateAsync(It.IsAny<Guid>(), It.IsAny<TestEntity>())).ReturnsAsync(_entity);
            var crudService = crudServiceMock.Object;
            var service = new HistoricalCrudService<Guid, TestEntity>(crudService, repository.Object, null);

            var result = service.Update(_entity.Id, _entity);

            Assert.Equal(_entity.Id, result.Id);
            repository.Verify(_ => _.CreateAsync<Guid, HistoricalEvent>(It.IsAny<HistoricalEvent>()), Times.Once);
            repository.Verify(_ => _.SaveChangesAsync(), Times.Once);
            crudServiceMock.Verify(_ => _.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            crudServiceMock.Verify(_ => _.UpdateAsync(It.IsAny<Guid>(), It.IsAny<TestEntity>()), Times.Once);
        }

        [Fact]
        public void Delete_ReturnsDeletedElement()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>())).ReturnsAsync(_entity);
            var crudServiceMock = new Mock<ICrudService<TestEntity>>();
             crudServiceMock.Setup(_ => _.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(_entity);
            var crudService = crudServiceMock.Object;
            var service = new HistoricalCrudService<Guid, TestEntity>(crudService, repository.Object, null);

            var result = service.Delete(_entity.Id);

            Assert.Equal(_entity.Id, result.Id);
            repository.Verify(_ => _.CreateAsync<Guid, HistoricalEvent>(It.IsAny<HistoricalEvent>()), Times.Once);
            crudServiceMock.Verify(_ => _.DeleteAsync(It.IsAny<Guid>()), Times.Once);
            repository.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public void Restore_ReturnsCreatedElement()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetAllAsync<Guid, HistoricalEvent>()).ReturnsAsync(_events);
            repository.Setup(_ => _.CreateAsync(It.IsAny<TestEntity>())).ReturnsAsync(_entity);
            var crudServiceMock = new Mock<ICrudService<TestEntity>>();
            crudServiceMock.Setup(_ => _.CreateAsync(It.IsAny<TestEntity>())).ReturnsAsync(_entity);
            var crudService = crudServiceMock.Object;
            var service = new HistoricalCrudService<Guid, TestEntity>(crudService, repository.Object, null);

            var result = service.Restore(_entity.Id);

            Assert.Equal(_entity.Id, result.Id);
            crudServiceMock.Verify(_ => _.CreateAsync(It.IsAny<TestEntity>()), Times.Once);
        }

        [Fact]
        public void Restore_ThrowsEntityNotFoundException()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetAllAsync<HistoricalEvent>()).ReturnsAsync(new List<HistoricalEvent>());
            var crudServiceMock = new Mock<ICrudService<TestEntity>>();
            var crudService = crudServiceMock.Object;
            var service = new HistoricalCrudService<Guid, TestEntity>(crudService, repository.Object, null);

            Assert.Throws<EntityNotFoundException>(() => service.Restore(_entity.Id));
        }

        [Fact]
        public void GetHistory_ReturnsHistory()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetAllAsync<Guid, HistoricalEvent>()).ReturnsAsync(_events);
            var crudServiceMock = new Mock<ICrudService<TestEntity>>();
            var crudService = crudServiceMock.Object;
            var service = new HistoricalCrudService<Guid, TestEntity>(crudService, repository.Object, null);

            var result = service.GetHistory(_entity.Id);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetHistory_ThrowsEntityNotFoundException()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetAllAsync<HistoricalEvent>()).ReturnsAsync(new List<HistoricalEvent>());
            var crudServiceMock = new Mock<ICrudService<TestEntity>>();
            var crudService = crudServiceMock.Object;
            var service = new HistoricalCrudService<Guid, TestEntity>(crudService, repository.Object, null);

            Assert.Throws<EntityNotFoundException>(() => service.GetHistory(_entity.Id));
        }

    }
}
