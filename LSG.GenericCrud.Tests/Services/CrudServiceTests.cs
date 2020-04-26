using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bogus;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using LSG.GenericCrud.Tests.Models;
using Moq;
using Xunit;

namespace LSG.GenericCrud.Tests.Services
{
    public class CrudServiceTests
    {
        private readonly IQueryable<TestEntity> _entities;
        private readonly TestEntity _entity;

        public CrudServiceTests()
        {
            Randomizer.Seed = new Random(1234567);
            var entityFaker = new Faker<TestEntity>().
                RuleFor(_ => _.Id, Guid.NewGuid()).
                RuleFor(_ => _.Value, _ => _.Lorem.Word());
            _entities = entityFaker.Generate(5).AsQueryable();
            _entity = entityFaker.Generate();
        }

        [Fact]
        public void Constructor_SetAutoCommitToTrue()
        {
            var service = new CrudServiceBase<Guid, TestEntity>(null);

            Assert.True(service.AutoCommit);
        }

        [Fact]
        public async void GetAllAsync_ReturnElements()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.GetAllAsync<Guid, TestEntity>()).ReturnsAsync(_entities);
            var service = new CrudServiceBase<Guid, TestEntity>(repositoryMock.Object);

            var result = await service.GetAllAsync();

            Assert.Equal(result.Count(), _entities.Count());
            repositoryMock.Verify(_ => _.GetAllAsync<Guid, TestEntity>(), Times.Once);
        }

        [Fact]
        public async void GetByIdAsync_ReturnOneElement()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>())).ReturnsAsync(_entity);
            var service = new CrudServiceBase<Guid, TestEntity>(repositoryMock.Object);

            var result = await service.GetByIdAsync(Guid.Empty);

            Assert.Equal(result.Id, _entity.Id);
            repositoryMock.Verify(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>()), Times.Once);
        }


        [Fact]
        public async void GetByIdAsync_ThrowsEntityNotFoundException()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>())).ReturnsAsync(default(TestEntity));
            var service = new CrudServiceBase<Guid, TestEntity>(repositoryMock.Object);

            await Assert.ThrowsAsync<EntityNotFoundException>(() => service.GetByIdAsync(Guid.Empty));
        }

        [Fact]
        public async void CreateAsync_ReturnsCreatedElement()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.CreateAsync<Guid, TestEntity>(It.IsAny<TestEntity>())).ReturnsAsync(_entity);
            var service = new CrudServiceBase<Guid, TestEntity>(repositoryMock.Object);

            var result = await service.CreateAsync(_entity);

            Assert.Equal(_entity.Id, result.Id);
            repositoryMock.Verify(_ => _.CreateAsync<Guid, TestEntity>(It.IsAny<TestEntity>()), Times.Once);
            repositoryMock.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }
        
        [Fact]
        public async void UpdateAsync_ReturnsUpdatedElement()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.GetByIdAsync<Guid, TestEntity>(_entity.Id)).ReturnsAsync(_entity);
            var service = new CrudServiceBase<Guid, TestEntity>(repositoryMock.Object);

            var result = await service.UpdateAsync(_entity.Id, _entity);

            Assert.Equal(_entity.Id, result.Id);
            repositoryMock.Verify(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>()), Times.Once());
            repositoryMock.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async void UpdateAsync_ThrowsEntityNotFoundException()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>())).ThrowsAsync(new EntityNotFoundException());
            var service = new CrudServiceBase<Guid, TestEntity>(repositoryMock.Object);

            await Assert.ThrowsAsync<EntityNotFoundException>(() => service.UpdateAsync(Guid.Empty, _entity));
        }

        [Fact]
        public async void DeleteAsync_ReturnsDeletedElement()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.GetByIdAsync<Guid, TestEntity>(_entity.Id)).ReturnsAsync(_entity);
            var service = new CrudServiceBase<Guid, TestEntity>(repositoryMock.Object);

            var result = await service.DeleteAsync(_entity.Id);

            Assert.Equal(_entity.Id, result.Id);
            repositoryMock.Verify(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>()), Times.Once());
            repositoryMock.Verify(_ => _.DeleteAsync<Guid, TestEntity>(It.IsAny<Guid>()), Times.Once);
            repositoryMock.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async void DeleteAsync_ThrowsEntityNotFoundException()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>())).ThrowsAsync(new EntityNotFoundException());
            var service = new CrudServiceBase<Guid, TestEntity>(repositoryMock.Object);

            await Assert.ThrowsAsync<EntityNotFoundException>(() => service.DeleteAsync(Guid.Empty));
        }

        [Fact]
        public async void CopyAsync_ReturnsCreatedElement()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>())).ReturnsAsync(_entity);
            repositoryMock.Setup(_ => _.CreateAsync<Guid, TestEntity>(It.IsAny<TestEntity>())).ReturnsAsync(_entity);
            var service = new CrudServiceBase<Guid, TestEntity>(repositoryMock.Object);

            var result = await service.CopyAsync(_entity.Id);

            Assert.Equal(_entity.Id, result.Id);
            repositoryMock.Verify(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>()), Times.Once);
            repositoryMock.Verify(_ => _.CreateAsync<Guid, TestEntity>(It.IsAny<TestEntity>()), Times.Once);
            repositoryMock.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }


        [Fact]
        public async void CopyAsync_ThrowsEntityNotFoundException()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>())).ReturnsAsync(default(TestEntity));
            var service = new CrudServiceBase<Guid, TestEntity>(repositoryMock.Object);

            await Assert.ThrowsAsync<EntityNotFoundException>(() => service.CopyAsync(Guid.Empty));
        }

    }
}
