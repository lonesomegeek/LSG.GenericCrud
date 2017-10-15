using System;
using System.Collections.Generic;
using Bogus;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Services;
using LSG.GenericCrud.Tests.Models;
using Moq;
using Xunit;

namespace LSG.GenericCrud.Tests.Controllers
{
    public class HistoricalCrudControllerTests
    {
        private readonly IList<TestEntity> _entities;
        private readonly TestEntity _entity;
        private readonly IList<HistoricalEvent> _events;

        public HistoricalCrudControllerTests()
        {
            Randomizer.Seed = new Random(1234567);
            var entityFaker = new Faker<TestEntity>().
                RuleFor(_ => _.Id, Guid.NewGuid()).
                RuleFor(_ => _.Value, _ => _.Lorem.Word());
            _entities = entityFaker.Generate(5);
            _entity = entityFaker.Generate();
            var historyFaker = new Faker<HistoricalEvent>();
            _events = historyFaker.Generate(2);
        }

        [Fact]
        public void Restore_ReturnsOk()
        {
            var serviceMock = new Mock<IHistoricalCrudService<TestEntity>>();
            serviceMock.Setup(_ => _.Restore(It.IsAny<Guid>())).Returns(_entity);
            var controller = new HistoricalCrudController<TestEntity>(serviceMock.Object);
        }
    }
}
