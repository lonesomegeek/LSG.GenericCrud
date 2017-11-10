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
            _events = new List<HistoricalEvent>() {eventFaker.Generate()};
        }

        [Fact]
        public void Constructor_SetAutoCommitToFalse()
        {
            var service = new HistoricalCrudService<TestEntity>(null);

            Assert.False(service.AutoCommit);
        }

        [Fact]
        public void Create_ReturnsCreatedElement()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.Create(It.IsAny<TestEntity>())).Returns(_entity);
            var service = new HistoricalCrudService<TestEntity>(repository.Object);

            var result = service.Create(_entity);

            Assert.Equal(_entity.Id, result.Id);
            repository.Verify(_ => _.Create(It.IsAny<HistoricalEvent>()), Times.Once);
            repository.Verify(_ => _.Create(It.IsAny<TestEntity>()), Times.Once);
            repository.Verify(_ => _.SaveChanges(), Times.Once);
        }

        [Fact]
        public async void CreateAsync_ReturnsCreatedElement()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.CreateAsync(It.IsAny<TestEntity>())).ReturnsAsync(_entity);
            var service = new HistoricalCrudService<TestEntity>(repository.Object);

            var result = await service.CreateAsync(_entity);

            Assert.Equal(_entity.Id, result.Id);
            repository.Verify(_ => _.CreateAsync(It.IsAny<HistoricalEvent>()), Times.Once);
            repository.Verify(_ => _.CreateAsync(It.IsAny<TestEntity>()), Times.Once);
            repository.Verify(_ => _.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Update_ReturnsUpdatedElement()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetById<TestEntity>(It.IsAny<Guid>())).Returns(_entity);
            var service = new HistoricalCrudService<TestEntity>(repository.Object);

            var result = service.Update(_entity.Id, _entity);

            Assert.Equal(_entity.Id, result.Id);
            repository.Verify(_ => _.Create(It.IsAny<HistoricalEvent>()), Times.Once);
            repository.Verify(_ => _.SaveChanges(), Times.Once);
        }

        [Fact]
        public async void UpdateAsync_ReturnsUpdatedElement()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetByIdAsync<TestEntity>(It.IsAny<Guid>())).ReturnsAsync(_entity);
            var service = new HistoricalCrudService<TestEntity>(repository.Object);

            var result = await service.UpdateAsync(_entity.Id, _entity);

            Assert.Equal(_entity.Id, result.Id);
            repository.Verify(_ => _.CreateAsync(It.IsAny<HistoricalEvent>()), Times.Once);
            repository.Verify(_ => _.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Delete_ReturnsDeletedElement()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetById<TestEntity>(It.IsAny<Guid>())).Returns(_entity);
            var service = new HistoricalCrudService<TestEntity>(repository.Object);

            var result = service.Delete(_entity.Id);

            Assert.Equal(_entity.Id, result.Id);
            repository.Verify(_ => _.Create(It.IsAny<HistoricalEvent>()), Times.Once);
            repository.Verify(_ => _.Delete<TestEntity>(It.IsAny<Guid>()), Times.Once);
            repository.Verify(_ => _.SaveChanges(), Times.Once);
        }

        [Fact]
        public async void DeleteAsync_ReturnsDeletedElement()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetByIdAsync<TestEntity>(It.IsAny<Guid>())).ReturnsAsync(_entity);
            var service = new HistoricalCrudService<TestEntity>(repository.Object);

            var result = await service.DeleteAsync(_entity.Id);

            Assert.Equal(_entity.Id, result.Id);
            repository.Verify(_ => _.CreateAsync(It.IsAny<HistoricalEvent>()), Times.Once);
            repository.Verify(_ => _.DeleteAsync<TestEntity>(It.IsAny<Guid>()), Times.Once);
            repository.Verify(_ => _.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Restore_ReturnsCreatedElement()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetAll<HistoricalEvent>()).Returns(_events);
            repository.Setup(_ => _.Create(It.IsAny<TestEntity>())).Returns(_entity);
            var service = new HistoricalCrudService<TestEntity>(repository.Object);

            var result = service.Restore(_entity.Id);
            
            Assert.Equal(_entity.Id, result.Id);
            repository.Verify(_ => _.Create(It.IsAny<TestEntity>()), Times.Once);
        }

        [Fact]
        public async void RestoreAsync_ReturnsCreatedElement()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetAllAsync<HistoricalEvent>()).ReturnsAsync(_events);
            repository.Setup(_ => _.CreateAsync(It.IsAny<TestEntity>())).ReturnsAsync(_entity);
            var service = new HistoricalCrudService<TestEntity>(repository.Object);

            var result = await service.RestoreAsync(_entity.Id);

            Assert.Equal(_entity.Id, result.Id);
            repository.Verify(_ => _.CreateAsync(It.IsAny<TestEntity>()), Times.Once);
        }

        [Fact]
        public void Restore_ThrowsEntityNotFoundException()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetAll<HistoricalEvent>()).Returns(new List<HistoricalEvent>());
            var service = new HistoricalCrudService<TestEntity>(repository.Object);

            Assert.Throws<EntityNotFoundException>(() => service.Restore(_entity.Id));
        }

        [Fact]
        public async void RestoreAsync_ThrowsEntityNotFoundException()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetAllAsync<HistoricalEvent>()).ReturnsAsync(new List<HistoricalEvent>());
            var service = new HistoricalCrudService<TestEntity>(repository.Object);

            await Assert.ThrowsAsync<EntityNotFoundException>(() => service.RestoreAsync(_entity.Id));
        }

        [Fact]
        public void GetHistory_ReturnsHistory()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetAll<HistoricalEvent>()).Returns(_events);
            var service = new HistoricalCrudService<TestEntity>(repository.Object);

            var result = service.GetHistory(_entity.Id);

            Assert.NotEmpty(result);
        }

        [Fact]
        public async void GetHistoryAsync_ReturnsHistory()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetAllAsync<HistoricalEvent>()).ReturnsAsync(_events);
            var service = new HistoricalCrudService<TestEntity>(repository.Object);

            var result = await service.GetHistoryAsync(_entity.Id);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetHistory_ThrowsEntityNotFoundException()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetAll<HistoricalEvent>()).Returns(new List<HistoricalEvent>());
            var service = new HistoricalCrudService<TestEntity>(repository.Object);

            Assert.Throws<EntityNotFoundException>(() => service.GetHistory(_entity.Id));
        }

        [Fact]
        public async void GetHistoryAsync_ThrowsEntityNotFoundException()
        {
            var repository = new Mock<CrudRepository>();
            repository.Setup(_ => _.GetAllAsync<HistoricalEvent>()).ReturnsAsync(new List<HistoricalEvent>());
            var service = new HistoricalCrudService<TestEntity>(repository.Object);

            await Assert.ThrowsAsync<EntityNotFoundException>(() => service.GetHistoryAsync(_entity.Id));
        }


    }
}
