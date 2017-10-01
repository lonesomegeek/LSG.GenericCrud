using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Tests.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Xunit;

namespace LSG.GenericCrud.Tests.Repositories
{
    public class CrudTests
    {

        [Fact]
        public void Test()
        {
            var contextMock = new Mock<IDbContext>();
            var set = new TestSet();
            contextMock.Setup(_ => _.Set<TestEntity>()).Returns(set);
            var crud = new Crud<TestEntity>(contextMock.Object);

            var result = crud.GetAll();

        }

        [Fact]
        public void GetById_ThrowsEntityNotFoundException()
        {
            //var setMock = new Mock<DbSet<TestEntity>>();
            //var contextMock = new Mock<IDbContext>();
            //contextMock.Setup(_ => _.Set<TestEntity>()).Returns(setMock.Object);
            var entity = new TestEntity() { Id = Guid.NewGuid() };
            var options = new DbContextOptionsBuilder<TestContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var context = new TestContext(options, null);
            var crud = new Crud<TestEntity>(context);

            var ex = Assert.Throws<EntityNotFoundException>(() => crud.GetById(Guid.NewGuid()));
        }

        //[Fact]
        public void Create_Test()
        {
            var entityMock = new Mock<EntityEntry<TestEntity>>();
            entityMock.Setup(_ => _.Entity).Returns(new TestEntity());
            var setMock = new Mock<DbSet<TestEntity>>();
            setMock.Setup(_ => _.Add(It.IsAny<TestEntity>())).Returns(entityMock.Object);
            var contextMock = new Mock<IDbContext>();
            contextMock.Setup(_ => _.Set<TestEntity>()).Returns(setMock.Object);
            var crud = new Crud<TestEntity>(contextMock.Object);
            // disable auto commit
            crud.AutoCommit = false;

            var result = crud.Create(new TestEntity());

            contextMock.Verify(_ => _.SaveChanges(), Times.Never);
        }

    }
}
