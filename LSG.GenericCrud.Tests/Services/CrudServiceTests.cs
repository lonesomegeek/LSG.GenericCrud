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
        public void Constructor_SetAutoCommitToTrue()
        {
            var service = new CrudService<Guid, TestEntity>(null);

            Assert.True(service.AutoCommit);
        }

        [Fact]
        public void GetAll_ReturnElements()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.GetAllAsync<Guid, TestEntity>()).ReturnsAsync(_entities);
            var service = new CrudService<Guid, TestEntity>(repositoryMock.Object);

            var result = service.GetAll();

            Assert.Equal(result.Count(), _entities.Count);
            repositoryMock.Verify(_ => _.GetAllAsync<Guid, TestEntity>(), Times.Once);
        }

        [Fact]
        public async void GetAllAsync_ReturnElements()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.GetAllAsync<Guid, TestEntity>()).ReturnsAsync(_entities);
            var service = new CrudService<Guid, TestEntity>(repositoryMock.Object);

            var result = await service.GetAllAsync();

            Assert.Equal(result.Count(), _entities.Count);
            repositoryMock.Verify(_ => _.GetAllAsync<Guid, TestEntity>(), Times.Once);
        }

        [Fact]
        public void GetById_ReturnOneElement()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>())).ReturnsAsync(_entity);
            var service = new CrudService<Guid, TestEntity>(repositoryMock.Object);

            var result = service.GetById(Guid.Empty);

            Assert.Equal(result.Id, _entity.Id);
            repositoryMock.Verify(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void GetByIdAsync_ReturnOneElement()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>())).ReturnsAsync(_entity);
            var service = new CrudService<Guid, TestEntity>(repositoryMock.Object);

            var result = await service.GetByIdAsync(Guid.Empty);

            Assert.Equal(result.Id, _entity.Id);
            repositoryMock.Verify(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void GetById_ThrowsEntityNotFoundException()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.GetById<TestEntity>(It.IsAny<Guid>())).Returns(default(TestEntity));
            var service = new CrudService<Guid, TestEntity>(repositoryMock.Object);

            Assert.Throws<EntityNotFoundException>(() => service.GetById(Guid.Empty));
        }

        [Fact]
        public async void GetByIdAsync_ThrowsEntityNotFoundException()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>())).ReturnsAsync(default(TestEntity));
            var service = new CrudService<Guid, TestEntity>(repositoryMock.Object);

            await Assert.ThrowsAsync<EntityNotFoundException>(() => service.GetByIdAsync(Guid.Empty));
        }

        [Fact]
        public void Create_ReturnsCreatedElement()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.CreateAsync<Guid, TestEntity>(It.IsAny<TestEntity>())).ReturnsAsync(_entity);
            var service = new CrudService<Guid, TestEntity>(repositoryMock.Object);

            var result = service.Create(_entity);

            Assert.Equal(_entity.Id, result.Id);
            repositoryMock.Verify(_ => _.CreateAsync<Guid, TestEntity>(It.IsAny<TestEntity>()), Times.Once);
            repositoryMock.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async void CreateAsync_ReturnsCreatedElement()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.CreateAsync<Guid, TestEntity>(It.IsAny<TestEntity>())).ReturnsAsync(_entity);
            var service = new CrudService<Guid, TestEntity>(repositoryMock.Object);

            var result = await service.CreateAsync(_entity);

            Assert.Equal(_entity.Id, result.Id);
            repositoryMock.Verify(_ => _.CreateAsync<Guid, TestEntity>(It.IsAny<TestEntity>()), Times.Once);
            repositoryMock.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public void Update_ReturnsUpdatedElement()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.GetByIdAsync<Guid, TestEntity>(_entity.Id)).ReturnsAsync(_entity);
            var service = new CrudService<Guid, TestEntity>(repositoryMock.Object);

            var result = service.Update(_entity.Id, _entity);

            Assert.Equal(_entity.Id, result.Id);
            repositoryMock.Verify(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>()), Times.Once());
            repositoryMock.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }
        
        [Fact]
        public async void UpdateAsync_ReturnsUpdatedElement()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.GetByIdAsync<Guid, TestEntity>(_entity.Id)).ReturnsAsync(_entity);
            var service = new CrudService<Guid, TestEntity>(repositoryMock.Object);

            var result = await service.UpdateAsync(_entity.Id, _entity);

            Assert.Equal(_entity.Id, result.Id);
            repositoryMock.Verify(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>()), Times.Once());
            repositoryMock.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public void Update_ThrowsEntityNotFoundException()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.GetById<TestEntity>(It.IsAny<Guid>())).Throws<EntityNotFoundException>();
            var service = new CrudService<Guid, TestEntity>(repositoryMock.Object);

            Assert.Throws<EntityNotFoundException>(() => service.Update(Guid.Empty, _entity));
        }

        [Fact]
        public async void UpdateAsync_ThrowsEntityNotFoundException()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>())).ThrowsAsync(new EntityNotFoundException());
            var service = new CrudService<Guid, TestEntity>(repositoryMock.Object);

            await Assert.ThrowsAsync<EntityNotFoundException>(() => service.UpdateAsync(Guid.Empty, _entity));
        }

        [Fact]
        public void Delete_ReturnsDeletedElement()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.GetByIdAsync<Guid, TestEntity>(_entity.Id)).ReturnsAsync(_entity);
            var service = new CrudService<Guid, TestEntity>(repositoryMock.Object);

            var result = service.Delete(_entity.Id);

            Assert.Equal(_entity.Id, result.Id);
            repositoryMock.Verify(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>()), Times.Once());
            repositoryMock.Verify(_ => _.DeleteAsync<Guid, TestEntity>(It.IsAny<Guid>()), Times.Once);
            repositoryMock.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async void DeleteAsync_ReturnsDeletedElement()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.GetByIdAsync<Guid, TestEntity>(_entity.Id)).ReturnsAsync(_entity);
            var service = new CrudService<Guid, TestEntity>(repositoryMock.Object);

            var result = await service.DeleteAsync(_entity.Id);

            Assert.Equal(_entity.Id, result.Id);
            repositoryMock.Verify(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>()), Times.Once());
            repositoryMock.Verify(_ => _.DeleteAsync<Guid, TestEntity>(It.IsAny<Guid>()), Times.Once);
            repositoryMock.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public void Delete_ThrowsEntityNotFoundException()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.GetById<TestEntity>(It.IsAny<Guid>())).Throws<EntityNotFoundException>();
            var service = new CrudService<Guid, TestEntity>(repositoryMock.Object);

            Assert.Throws<EntityNotFoundException>(() => service.Delete(Guid.Empty));
        }

        [Fact]
        public async void DeleteAsync_ThrowsEntityNotFoundException()
        {
            var repositoryMock = new Mock<ICrudRepository>();
            repositoryMock.Setup(_ => _.GetByIdAsync<Guid, TestEntity>(It.IsAny<Guid>())).ThrowsAsync(new EntityNotFoundException());
            var service = new CrudService<Guid, TestEntity>(repositoryMock.Object);

            await Assert.ThrowsAsync<EntityNotFoundException>(() => service.DeleteAsync(Guid.Empty));
        }
    }
}
