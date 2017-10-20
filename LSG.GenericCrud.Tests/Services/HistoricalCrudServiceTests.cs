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
            var service = new HistoricalCrudService<TestEntity>(null, null);

            Assert.False(service.AutoCommit);
        }

        [Fact]
        public void Create_ReturnsCreatedElement()
        {
            var eventRepositoryMock = new Mock<CrudRepository<HistoricalEvent>>();
            var entityRepositoryMock = new Mock<CrudRepository<TestEntity>>();
            entityRepositoryMock.Setup(_ => _.Create(It.IsAny<TestEntity>())).Returns(_entity);
            var service = new HistoricalCrudService<TestEntity>(entityRepositoryMock.Object, eventRepositoryMock.Object);

            var result = service.Create(_entity);

            Assert.Equal(_entity.Id, result.Id);
            eventRepositoryMock.Verify(_ => _.Create(It.IsAny<HistoricalEvent>()), Times.Once);
            entityRepositoryMock.Verify(_ => _.Create(It.IsAny<TestEntity>()), Times.Once);
            entityRepositoryMock.Verify(_ => _.SaveChanges(), Times.Once);
            eventRepositoryMock.Verify(_ => _.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Delete_ReturnsDeletedElement()
        {
            var eventRepositoryMock = new Mock<CrudRepository<HistoricalEvent>>();
            var entityRepositoryMock = new Mock<CrudRepository<TestEntity>>();
            entityRepositoryMock.Setup(_ => _.GetById(It.IsAny<Guid>())).Returns(_entity);
            var service = new HistoricalCrudService<TestEntity>(entityRepositoryMock.Object, eventRepositoryMock.Object);

            var result = service.Delete(_entity.Id);

            Assert.Equal(_entity.Id, result.Id);
            eventRepositoryMock.Verify(_ => _.Create(It.IsAny<HistoricalEvent>()), Times.Once);
            entityRepositoryMock.Verify(_ => _.Delete(It.IsAny<Guid>()), Times.Once);
            entityRepositoryMock.Verify(_ => _.SaveChanges(), Times.Once);
            eventRepositoryMock.Verify(_ => _.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Update_ReturnsUpdatedElement()
        {
            var eventRepositoryMock = new Mock<CrudRepository<HistoricalEvent>>();
            var entityRepositoryMock = new Mock<CrudRepository<TestEntity>>();
            entityRepositoryMock.Setup(_ => _.GetById(It.IsAny<Guid>())).Returns(_entity);
            var service = new HistoricalCrudService<TestEntity>(entityRepositoryMock.Object, eventRepositoryMock.Object);

            var result = service.Update(_entity.Id, _entity);

            Assert.Equal(_entity.Id, result.Id);
            eventRepositoryMock.Verify(_ => _.Create(It.IsAny<HistoricalEvent>()), Times.Once);
            entityRepositoryMock.Verify(_ => _.SaveChanges(), Times.Once);
            eventRepositoryMock.Verify(_ => _.SaveChanges(), Times.Once);
        }


        [Fact]
        public void Restore_ReturnsCreatedElement()
        {
            var eventRepositoryMock = new Mock<CrudRepository<HistoricalEvent>>();
            eventRepositoryMock.Setup(_ => _.GetAll()).Returns(_events);
            var entityRepositoryMock = new Mock<CrudRepository<TestEntity>>();
            entityRepositoryMock.Setup(_ => _.Create(It.IsAny<TestEntity>())).Returns(_entity);
            var service = new HistoricalCrudService<TestEntity>(entityRepositoryMock.Object, eventRepositoryMock.Object);

            var result = service.Restore(_entity.Id);
            
            Assert.Equal(_entity.Id, result.Id);
            entityRepositoryMock.Verify(_ => _.Create(It.IsAny<TestEntity>()), Times.Once);
        }

        [Fact]
        public void Restore_ThrowsEntityNotFoundException()
        {
            var eventRepositoryMock = new Mock<CrudRepository<HistoricalEvent>>();
            eventRepositoryMock.Setup(_ => _.GetAll()).Returns(new List<HistoricalEvent>());
            var entityRepositoryMock = new Mock<CrudRepository<TestEntity>>();
            var service = new HistoricalCrudService<TestEntity>(entityRepositoryMock.Object, eventRepositoryMock.Object);

            Assert.Throws<EntityNotFoundException>(() => service.Restore(_entity.Id));
        }

        [Fact]
        public void GetHistory_ReturnsHistory()
        {
            var eventRepositoryMock = new Mock<CrudRepository<HistoricalEvent>>();
            eventRepositoryMock.Setup(_ => _.GetAll()).Returns(_events);
            var service = new HistoricalCrudService<TestEntity>(null, eventRepositoryMock.Object);

            var result = service.GetHistory(_entity.Id);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetHistory_ThrowsEntityNotFoundException()
        {
            var eventRepositoryMock = new Mock<CrudRepository<HistoricalEvent>>();
            eventRepositoryMock.Setup(_ => _.GetAll()).Returns(new List<HistoricalEvent>());
            var service = new HistoricalCrudService<TestEntity>(null, eventRepositoryMock.Object);

            Assert.Throws<EntityNotFoundException>(() => service.GetHistory(_entity.Id));
        }


    }
}
