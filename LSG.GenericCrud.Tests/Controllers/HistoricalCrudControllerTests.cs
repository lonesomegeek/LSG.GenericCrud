using System;
using System.Collections.Generic;
using Bogus;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Services;
using LSG.GenericCrud.Tests.Models;
using Microsoft.AspNetCore.Mvc;
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
        public async void RestoreAsync_ReturnsOk()
        {
            var serviceMock = new Mock<IHistoricalCrudService<TestEntity>>();
            serviceMock.Setup(_ => _.RestoreAsync(It.IsAny<Guid>())).ReturnsAsync(_entity);
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var result = await controller.RestoreFromDeletedEntity(_entity.Id);

            Assert.IsType<OkObjectResult>(result);
            serviceMock.Verify(_ => _.RestoreAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void Restore_ThrowsEntityNotFoundException()
        {
            var serviceMock = new Mock<IHistoricalCrudService<TestEntity>>();
            serviceMock.Setup(_ => _.RestoreAsync(It.IsAny<Guid>())).ThrowsAsync(new EntityNotFoundException());
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var result = await controller.RestoreFromDeletedEntity(_entity.Id);

            Assert.IsType<NotFoundResult>(result);
            serviceMock.Verify(_ => _.RestoreAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void GetHistory_ReturnsOk()
        {
            var serviceMock = new Mock<IHistoricalCrudService<TestEntity>>();
            serviceMock.Setup(_ => _.GetHistoryAsync(It.IsAny<Guid>())).ReturnsAsync(_events);
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var result = await controller.GetHistory(_entity.Id);

            Assert.IsType<OkObjectResult>(result);
            serviceMock.Verify(_ => _.GetHistoryAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void GetHistory_ThrowsEntityNotFoundException()
        {
            var serviceMock = new Mock<IHistoricalCrudService<TestEntity>>();
            serviceMock.Setup(_ => _.GetHistoryAsync(It.IsAny<Guid>())).ThrowsAsync(new EntityNotFoundException());
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);
            
            var result = await controller.GetHistory(_entity.Id);

            Assert.IsType<NotFoundResult>(result);
            serviceMock.Verify(_ => _.GetHistoryAsync(It.IsAny<Guid>()), Times.Once);
        }


    }
}
