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
                RuleFor(_ => _.EntityId, _entity.Id).
                RuleFor(_ => _.Changeset, "{}");
            _events = new List<HistoricalEvent>() { eventFaker.Generate() };
        }

        [Fact]
        public void Constructor_SetAutoCommitToFalse()
        {
            var crudService = new CrudService<TestEntity>(null);
            var service = new HistoricalCrudService<TestEntity>(crudService, null);

            Assert.False(service.AutoCommit);
            Assert.False(crudService.AutoCommit);
        }

        [Fact]
        public void Create_ReturnsCreatedElement()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.CreateAsync(It.IsAny<TestEntity>())).ReturnsAsync(_entity);
            var crudService = new CrudService<TestEntity>(repository.Object);
            var service = new HistoricalCrudService<TestEntity>(crudService, repository.Object);

            var result = service.Create(_entity);

            Assert.Equal(_entity.Id, result.Id);
            repository.Verify(_ => _.CreateAsync(It.IsAny<HistoricalEvent>()), Times.Once);
            repository.Verify(_ => _.CreateAsync(It.IsAny<TestEntity>()), Times.Once);
            repository.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public void Update_ReturnsUpdatedElement()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetByIdAsync<TestEntity>(It.IsAny<Guid>())).ReturnsAsync(_entity);
            var crudService = new CrudService<TestEntity>(repository.Object);
            var service = new HistoricalCrudService<TestEntity>(crudService, repository.Object);

            var result = service.Update(_entity.Id, _entity);

            Assert.Equal(_entity.Id, result.Id);
            repository.Verify(_ => _.CreateAsync(It.IsAny<HistoricalEvent>()), Times.Once);
            repository.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public void Delete_ReturnsDeletedElement()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetByIdAsync<TestEntity>(It.IsAny<Guid>())).ReturnsAsync(_entity);
            var crudService = new CrudService<TestEntity>(repository.Object);
            var service = new HistoricalCrudService<TestEntity>(crudService, repository.Object);

            var result = service.Delete(_entity.Id);

            Assert.Equal(_entity.Id, result.Id);
            repository.Verify(_ => _.CreateAsync(It.IsAny<HistoricalEvent>()), Times.Once);
            repository.Verify(_ => _.DeleteAsync<TestEntity>(It.IsAny<Guid>()), Times.Once);
            repository.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public void Restore_ReturnsCreatedElement()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetAllAsync<HistoricalEvent>()).ReturnsAsync(_events);
            repository.Setup(_ => _.CreateAsync(It.IsAny<TestEntity>())).ReturnsAsync(_entity);
            var crudService = new CrudService<TestEntity>(repository.Object);
            var service = new HistoricalCrudService<TestEntity>(crudService, repository.Object);
            var result = service.Restore(_entity.Id);

            Assert.Equal(_entity.Id, result.Id);
            repository.Verify(_ => _.CreateAsync(It.IsAny<TestEntity>()), Times.Once);
        }

        [Fact]
        public void Restore_ThrowsEntityNotFoundException()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetAllAsync<HistoricalEvent>()).ReturnsAsync(new List<HistoricalEvent>());
            var crudService = new CrudService<TestEntity>(repository.Object);
            var service = new HistoricalCrudService<TestEntity>(crudService, repository.Object);

            Assert.Throws<EntityNotFoundException>(() => service.Restore(_entity.Id));
        }

        [Fact]
        public void GetHistory_ReturnsHistory()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetAllAsync<HistoricalEvent>()).ReturnsAsync(_events);
            var crudService = new CrudService<TestEntity>(repository.Object);
            var service = new HistoricalCrudService<TestEntity>(crudService, repository.Object);

            var result = service.GetHistory(_entity.Id);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetHistory_ThrowsEntityNotFoundException()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetAllAsync<HistoricalEvent>()).ReturnsAsync(new List<HistoricalEvent>());
            var crudService = new CrudService<TestEntity>(repository.Object);
            var service = new HistoricalCrudService<TestEntity>(crudService, repository.Object);

            Assert.Throws<EntityNotFoundException>(() => service.GetHistory(_entity.Id));
        }

    }
}
