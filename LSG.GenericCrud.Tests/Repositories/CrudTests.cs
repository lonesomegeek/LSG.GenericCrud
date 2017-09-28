using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Tests.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace LSG.GenericCrud.Tests.Repositories
{
    public class CrudTests
    {
        private readonly IList<TestEntity> _entities;
        private readonly TestEntity _entity;

        public CrudTests()
        {
            Randomizer.Seed = new Random(1234567);
            var entityFaker = new Faker<TestEntity>().
                RuleFor(_ => _.Id, Guid.NewGuid()).
                RuleFor(_ => _.Value, _ => _.Lorem.Word());
            _entities = entityFaker.Generate(5);
            _entity = entityFaker.Generate();
        }

        [Fact]
        public void Test()
        {
            var contextMock = new Mock<IDbContext>();
            var set = new TestSet();
            contextMock.Setup(_ => _.Set<TestEntity>()).Returns(set);
            var crud = new Crud<TestEntity>(contextMock.Object);

            var result = crud.GetAll();

        }
    }
}
