using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
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
                RuleFor(_ => _.EntityId, _entity.Id);
            _events = new List<HistoricalEvent>() {eventFaker.Generate()};
        }

        [Fact]
        public void Constructor_SetAutoCommitToFalse()
        {
            var service = new HistoricalCrudService<TestEntity>(null, null);

            Assert.False(service.AutoCommit);
        }

        [Fact]
        public void Restore_ReturnsCreatedElement()
        {
            var eventRepositoryMock = new Mock<CrudRepository<HistoricalEvent>>();
            eventRepositoryMock.Setup(_ => _.GetAll()).Returns(_events);
            var entityRepositoryMock = new Mock<CrudRepository<TestEntity>>();
            var service = new HistoricalCrudService<TestEntity>(entityRepositoryMock.Object, eventRepositoryMock.Object);

            var result = service.Restore(_entity.Id);
            
            entityRepositoryMock.Verify(_ => _.Create(It.IsAny<TestEntity>()), Times.Once);
        }
    }
}
