using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bogus;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using LSG.GenericCrud.Tests.Models;
using Moq;
using Xunit;

namespace LSG.GenericCrud.Tests.Services
{
    public class CrudServiceTests
    {
        private readonly IList<TestEntity> _entities;
        private readonly TestEntity _entity;

        public CrudServiceTests()
        {
            Randomizer.Seed = new Random(1234567);
            var entityFaker = new Faker<TestEntity>().
                RuleFor(_ => _.Id, Guid.NewGuid()).
                RuleFor(_ => _.Value, _ => _.Lorem.Word());
            _entities = entityFaker.Generate(5);
            _entity = entityFaker.Generate();
        }

        [Fact]
        public void GetAll_ReturnElements()
        {
            var repositoryMock = new Mock<ICrudRepository<TestEntity>>();
            repositoryMock.Setup(_ => _.GetAll()).Returns(_entities);
            var service = new CrudService<TestEntity>(repositoryMock.Object);

            var result = service.GetAll();

            Assert.Equal(result.Count(), _entities.Count);
            repositoryMock.Verify(_ => _.GetAll(), Times.Once);
        }

        [Fact]
        public void GetById_ReturnOneElement()
        {
            var repositoryMock = new Mock<ICrudRepository<TestEntity>>();
            repositoryMock.Setup(_ => _.GetById(It.IsAny<Guid>())).Returns(_entity);
            var service = new CrudService<TestEntity>(repositoryMock.Object);

            var result = service.GetById(Guid.Empty);

            Assert.Equal(result.Id, _entity.Id);
            repositoryMock.Verify(_ => _.GetById(It.IsAny<Guid>()), Times.Once);
        }
    }
}
