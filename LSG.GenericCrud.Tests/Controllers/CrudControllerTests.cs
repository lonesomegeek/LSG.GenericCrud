using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using LSG.GenericCrud.Controllers;
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
    }
}
