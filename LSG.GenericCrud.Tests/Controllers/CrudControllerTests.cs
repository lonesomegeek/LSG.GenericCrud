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
    public class CrudControllerTests
    {
        private readonly IList<TestEntity> _entities;
        private readonly TestEntity _entity;

        public CrudControllerTests()
        {
            Randomizer.Seed = new Random(1234567);
            var entityFaker = new Faker<TestEntity>().
                RuleFor(_ => _.Id, Guid.NewGuid()).
                RuleFor(_ => _.Value, _ => _.Lorem.Word());
            _entities = entityFaker.Generate(5);
            _entity = entityFaker.Generate();
        }

        [Fact]
        public void GetAll_ReturnsOk()
        {
            var serviceMock = new Mock<ICrudService<TestEntity>>();
            serviceMock.Setup(_ => _.GetAll()).Returns(_entities);
            var controller = new CrudController<TestEntity>(serviceMock.Object);

            var actionResult = controller.GetAll();
            var okResult = actionResult as OkObjectResult;
            var model = okResult.Value as IEnumerable<TestEntity>;

            Assert.Equal(model.Count(), _entities.Count);
            serviceMock.Verify(_ => _.GetAll(), Times.Once);
        }

        [Fact]
        public void GetById_ReturnsOk()
        {
            var id = _entities[0].Id;
            var serviceMock = new Mock<ICrudService<TestEntity>>();
            serviceMock.Setup(_ => _.GetById(id)).Returns(_entities[0]);
            var controller = new CrudController<TestEntity>(serviceMock.Object);

            var actionResult = controller.GetById(id);
            var okResult = actionResult as OkObjectResult;
            var model = okResult.Value as TestEntity;

            Assert.Equal(model.Id, id);
            serviceMock.Verify(_ => _.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void GetById_ReturnsNotFound()
        {
            var serviceMock = new Mock<ICrudService<TestEntity>>();
            serviceMock.Setup(_ => _.GetById(It.IsAny<Guid>())).Throws(new EntityNotFoundException());
            var controller = new CrudController<TestEntity>(serviceMock.Object);

            var actionResult = controller.GetById(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(actionResult);
            serviceMock.Verify(_ => _.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void Create_ReturnsCreatedEntity()
        {
            var serviceMock = new Mock<ICrudService<TestEntity>>();
            var controller = new CrudController<TestEntity>(serviceMock.Object);

            var actionResult = controller.Create(_entity);

            Assert.IsType<OkObjectResult>(actionResult);
            serviceMock.Verify(_ => _.Create(It.IsAny<TestEntity>()), Times.Once);
        }

        [Fact]
        public void Update_ReturnsModifiedEntity()
        {
            var serviceMock = new Mock<ICrudService<TestEntity>>();
            var controller = new CrudController<TestEntity>(serviceMock.Object);

            var actionResult = controller.Update(_entity.Id, _entity);

            Assert.IsType<OkObjectResult>(actionResult);
            serviceMock.Verify(_ => _.Update(It.IsAny<Guid>(), It.IsAny<TestEntity>()), Times.Once);
        }

        [Fact]
        public void Update_ReturnsNotFound()
        {
            var serviceMock = new Mock<ICrudService<TestEntity>>();
            serviceMock.Setup(_ => _.Update(It.IsAny<Guid>(), It.IsAny<TestEntity>())).Throws<EntityNotFoundException>();
            var controller = new CrudController<TestEntity>(serviceMock.Object);

            var actionResult = controller.Update(_entity.Id, _entity);

            Assert.IsType<NotFoundResult>(actionResult);
            serviceMock.Verify(_ => _.Update(It.IsAny<Guid>(), It.IsAny<TestEntity>()), Times.Once);
        }

        [Fact]
        public void Delete_ReturnsOk()
        {
            var serviceMock = new Mock<ICrudService<TestEntity>>();
            var controller = new CrudController<TestEntity>(serviceMock.Object);

            var actionResult = controller.Delete(_entity.Id);

            Assert.IsType<OkObjectResult>(actionResult);
            serviceMock.Verify(_ => _.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void Delete_ReturnsNotFound()
        {
            var serviceMock = new Mock<ICrudService<TestEntity>>();
            serviceMock.Setup(_ => _.Delete(It.IsAny<Guid>())).Throws<EntityNotFoundException>();
            var controller = new CrudController<TestEntity>(serviceMock.Object);

            var actionResult = controller.Delete(_entity.Id);

            Assert.IsType<NotFoundResult>(actionResult);
            serviceMock.Verify(_ => _.Delete(It.IsAny<Guid>()), Times.Once);
        }
    }
}
