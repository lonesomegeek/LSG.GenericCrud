using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Repositories;
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
        public async Task GetAll_ReturnsOk()
        {
            var dalMock = new Mock<Crud<TestEntity>>();
            dalMock.Setup(_ => _.GetAllAsync()).ReturnsAsync(_entities);
            var controller = new CrudAsyncController<TestEntity>(dalMock.Object);

            var actionResult = await controller.GetAll();
            var okResult = actionResult as OkObjectResult;
            var model = okResult.Value as IEnumerable<TestEntity>;

            Assert.Equal(model.Count(), _entities.Count);
            dalMock.Verify(_ => _.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetById_ReturnsOk()
        {
            var id = _entities[0].Id;
            var dalMock = new Mock<Crud<TestEntity>>();
            dalMock.Setup(_ => _.GetByIdAsync(id)).ReturnsAsync(_entities[0]);
            var controller = new CrudAsyncController<TestEntity>(dalMock.Object);

            var actionResult = await controller.GetById(id);
            var okResult = actionResult as OkObjectResult;
            var model = okResult.Value as TestEntity;

            Assert.Equal(model.Id, id);
            dalMock.Verify(_ => _.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound()
        {
            var dalMock = new Mock<Crud<TestEntity>>();
            dalMock.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ThrowsAsync(new EntityNotFoundException());
            var controller = new CrudAsyncController<TestEntity>(dalMock.Object);

            var actionResult = await controller.GetById(Guid.NewGuid());

            Assert.IsType(typeof(NotFoundResult), actionResult);
            dalMock.Verify(_ => _.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Create_ReturnsCreatedEntity()
        {
            var dalMock = new Mock<Crud<TestEntity>>();
            var controller = new CrudAsyncController<TestEntity>(dalMock.Object);

            var actionResult = await controller.Create(_entity);

            Assert.IsType<OkObjectResult>(actionResult);
            dalMock.Verify(_ => _.CreateAsync(It.IsAny<TestEntity>()), Times.Once);
        }

        [Fact]
        public async Task Update_ReturnsModifiedEntity()
        {
            var dalMock = new Mock<Crud<TestEntity>>();
            var controller = new CrudAsyncController<TestEntity>(dalMock.Object);

            var actionResult = await controller.Update(_entity.Id, _entity);

            Assert.IsType<OkResult>(actionResult);
            dalMock.Verify(_ => _.UpdateAsync(It.IsAny<Guid>(), It.IsAny<TestEntity>()), Times.Once);
        }

        [Fact]
        public async Task Update_ReturnsNotFound()
        {
            var dalMock = new Mock<Crud<TestEntity>>();
            dalMock.Setup(_ => _.UpdateAsync(It.IsAny<Guid>(), It.IsAny<TestEntity>())).ThrowsAsync(new EntityNotFoundException());
            var controller = new CrudAsyncController<TestEntity>(dalMock.Object);

            var actionResult = await controller.Update(_entity.Id, _entity);

            Assert.IsType(typeof(NotFoundResult), actionResult);
            dalMock.Verify(_ => _.UpdateAsync(It.IsAny<Guid>(), It.IsAny<TestEntity>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ReturnsOk()
        {
            var dalMock = new Mock<Crud<TestEntity>>();
            var controller = new CrudAsyncController<TestEntity>(dalMock.Object);

            var actionResult = await controller.Delete(_entity.Id);

            Assert.IsType(typeof(OkResult), actionResult);
            dalMock.Verify(_ => _.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound()
        {
            var dalMock = new Mock<Crud<TestEntity>>();
            dalMock.Setup(_ => _.DeleteAsync(It.IsAny<Guid>())).ThrowsAsync(new EntityNotFoundException());
            var controller = new CrudAsyncController<TestEntity>(dalMock.Object);

            var actionResult = await controller.Delete(_entity.Id);

            Assert.IsType(typeof(NotFoundResult), actionResult);
            dalMock.Verify(_ => _.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
