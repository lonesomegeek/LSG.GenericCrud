using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Services;
using LSG.GenericCrud.Tests.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LSG.GenericCrud.Tests.Controllers
{
    public class CrudAsyncControllerTests
    {
        private readonly IList<TestEntity> _entities;
        private readonly TestEntity _entity;

        public CrudAsyncControllerTests()
        {
            Randomizer.Seed = new Random(1234567);
            var entityFaker = new Faker<TestEntity>().
                RuleFor(_ => _.Id, Guid.NewGuid()).
                RuleFor(_ => _.Value, _ => _.Lorem.Word());
            _entities = entityFaker.Generate(5);
            _entity = entityFaker.Generate();
        }

        [Fact]
        public async void GetAll_ReturnsAsyncOk()
        {
            var serviceMock = new Mock<ICrudService<TestEntity>>();
            serviceMock.Setup(_ => _.GetAllAsync()).ReturnsAsync(_entities);
            var controller = new CrudAsyncController<TestEntity>(serviceMock.Object);

            var actionResult = await controller.GetAll();
            var okResult = actionResult as OkObjectResult;
            var model = okResult.Value as IEnumerable<TestEntity>;

            Assert.Equal(model.Count(), _entities.Count);
            serviceMock.Verify(_ => _.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async void GetById_ReturnsAsyncOk()
        {
            var id = _entities[0].Id;
            var serviceMock = new Mock<ICrudService<TestEntity>>();
            serviceMock.Setup(_ => _.GetByIdAsync(id)).ReturnsAsync(_entities[0]);
            var controller = new CrudAsyncController<TestEntity>(serviceMock.Object);

            var actionResult = await controller.GetById(id);
            var okResult = actionResult as OkObjectResult;
            var model = okResult.Value as TestEntity;

            Assert.Equal(model.Id, id);
            serviceMock.Verify(_ => _.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void GetById_ReturnsAsyncNotFound()
        {
            var serviceMock = new Mock<ICrudService<TestEntity>>();
            serviceMock.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).Throws(new EntityNotFoundException());
            var controller = new CrudAsyncController<TestEntity>(serviceMock.Object);

            var actionResult = await controller.GetById(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(actionResult);
            serviceMock.Verify(_ => _.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void Create_ReturnsAsyncCreatedEntity()
        {
            var serviceMock = new Mock<ICrudService<TestEntity>>();
            var controller = new CrudAsyncController<TestEntity>(serviceMock.Object);

            var actionResult = await controller.Create(_entity);

            Assert.IsType<OkObjectResult>(actionResult);
            serviceMock.Verify(_ => _.CreateAsync(It.IsAny<TestEntity>()), Times.Once);
        }

        [Fact]
        public async void Update_ReturnsAsyncModifiedEntity()
        {
            var serviceMock = new Mock<ICrudService<TestEntity>>();
            var controller = new CrudAsyncController<TestEntity>(serviceMock.Object);

            var actionResult = await controller.Update(_entity.Id, _entity);

            Assert.IsType<OkObjectResult>(actionResult);
            serviceMock.Verify(_ => _.UpdateAsync(It.IsAny<Guid>(), It.IsAny<TestEntity>()), Times.Once);
        }

        [Fact]
        public async void Update_ReturnsAsyncNotFound()
        {
            var serviceMock = new Mock<ICrudService<TestEntity>>();
            serviceMock.Setup(_ => _.UpdateAsync(It.IsAny<Guid>(), It.IsAny<TestEntity>())).Throws<EntityNotFoundException>();
            var controller = new CrudAsyncController<TestEntity>(serviceMock.Object);

            var actionResult = await controller.Update(_entity.Id, _entity);

            Assert.IsType<NotFoundResult>(actionResult);
            serviceMock.Verify(_ => _.UpdateAsync(It.IsAny<Guid>(), It.IsAny<TestEntity>()), Times.Once);
        }

        [Fact]
        public async void Delete_ReturnsAsyncOk()
        {
            var serviceMock = new Mock<ICrudService<TestEntity>>();
            var controller = new CrudAsyncController<TestEntity>(serviceMock.Object);

            var actionResult = await controller.Delete(_entity.Id);

            Assert.IsType<OkObjectResult>(actionResult);
            serviceMock.Verify(_ => _.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void Delete_ReturnsAsyncNotFound()
        {
            var serviceMock = new Mock<ICrudService<TestEntity>>();
            serviceMock.Setup(_ => _.DeleteAsync(It.IsAny<Guid>())).Throws<EntityNotFoundException>();
            var controller = new CrudAsyncController<TestEntity>(serviceMock.Object);

            var actionResult = await controller.Delete(_entity.Id);

            Assert.IsType<NotFoundResult>(actionResult);
            serviceMock.Verify(_ => _.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
