using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Bogus;
using LSG.GenericCrud.Dto.Services;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using LSG.GenericCrud.Tests.Models;
using Moq;
using Xunit;

namespace LSG.GenericCrud.Tests.Services
{
    public class CrudServiceDtoTests
    {
        private readonly IList<TestEntity> _entities;
        private readonly TestEntity _entity;
        private readonly IList<TestDto> _dtos;
        private readonly TestDto _dto;
        private readonly IMapper _mapper;

        public CrudServiceDtoTests()
        {
            Randomizer.Seed = new Random(1234567);
            var entityFaker = new Faker<TestEntity>().
                RuleFor(_ => _.Id, Guid.NewGuid()).
                RuleFor(_ => _.Value, _ => _.Lorem.Word());
            _entities = entityFaker.Generate(5);
            _entity = entityFaker.Generate();

            var dtoFaker = new Faker<TestDto>().
                RuleFor(_ => _.Id, Guid.NewGuid()).
                RuleFor(_ => _.ValueDto, _ => _.Lorem.Word());
            _dtos = dtoFaker.Generate(5);
            _dto = dtoFaker.Generate();

            _mapper = new AutoMapper.MapperConfiguration(_ =>
            {
                _.CreateMap<TestDto, TestEntity>().ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.ValueDto));
                _.CreateMap<TestEntity, TestDto>().ForMember(dest => dest.ValueDto, opts => opts.MapFrom(src => src.Value));

            }).CreateMapper();
        }

        [Fact]
        public void GetAll_ReturnElements()
        {
            var repositoryMock = new Mock<CrudRepository<TestEntity>>();
            repositoryMock.Setup(_ => _.GetAll()).Returns(_entities);
            var service = new CrudService<TestDto, TestEntity>(repositoryMock.Object, _mapper);

            var result = service.GetAll();

            Assert.Equal(_entities.Count, result.Count());
            repositoryMock.Verify(_ => _.GetAll(), Times.Once);
        }
        
        [Fact]
        public async void GetAllAsync_ReturnElements()
        {
            var repositoryMock = new Mock<CrudRepository<TestEntity>>();
            repositoryMock.Setup(_ => _.GetAllAsync()).ReturnsAsync(_entities);
            var service = new CrudService<TestDto, TestEntity>(repositoryMock.Object, _mapper);

            var result = await service.GetAllAsync();

            Assert.Equal(_entities.Count, result.Count());
            repositoryMock.Verify(_ => _.GetAllAsync(), Times.Once);
        }

        [Fact]
        public void GetById_ReturnElement()
        {
            var repositoryMock = new Mock<CrudRepository<TestEntity>>();
            repositoryMock.Setup(_ => _.GetById(It.IsAny<Guid>())).Returns(_entity);
            var service = new CrudService<TestDto, TestEntity>(repositoryMock.Object, _mapper);

            var result = service.GetById(_entity.Id);

            Assert.Equal(_entity.Id, result.Id);
            repositoryMock.Verify(_ => _.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void GetByIdAsync_ReturnElement()
        {
            var repositoryMock = new Mock<CrudRepository<TestEntity>>();
            repositoryMock.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(_entity);
            var service = new CrudService<TestDto, TestEntity>(repositoryMock.Object, _mapper);

            var result = await service.GetByIdAsync(_entity.Id);

            Assert.Equal(_entity.Id, result.Id);
            repositoryMock.Verify(_ => _.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void GetById_ThrowsEntityNotFoundException()
        {
            var repositoryMock = new Mock<CrudRepository<TestEntity>>();
            repositoryMock.Setup(_ => _.GetById(It.IsAny<Guid>())).Returns(default(TestEntity));
            var service = new CrudService<TestDto, TestEntity>(repositoryMock.Object, _mapper);

            Assert.Throws<EntityNotFoundException>(() => service.GetById(_entity.Id));
            repositoryMock.Verify(_ => _.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void GetByIdAsync_ThrowsEntityNotFoundException()
        {
            var repositoryMock = new Mock<CrudRepository<TestEntity>>();
            repositoryMock.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(default(TestEntity));
            var service = new CrudService<TestDto, TestEntity>(repositoryMock.Object, _mapper);

            await Assert.ThrowsAsync<EntityNotFoundException>(() => service.GetByIdAsync(_entity.Id));
            repositoryMock.Verify(_ => _.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void Create_ReturnsCreatedElement()
        {
            var repositoryMock = new Mock<ICrudRepository<TestEntity>>();
            repositoryMock.Setup(_ => _.Create(It.IsAny<TestEntity>())).Returns(_entity);
            var service = new CrudService<TestDto, TestEntity>(repositoryMock.Object, _mapper);

            var result = service.Create(_dto);

            Assert.Equal(_entity.Id, result.Id);
            repositoryMock.Verify(_ => _.Create(It.IsAny<TestEntity>()), Times.Once);
            repositoryMock.Verify(_ => _.SaveChanges(), Times.Once);
        }

        [Fact]
        public async void CreateAsync_ReturnsCreatedElement()
        {
            var repositoryMock = new Mock<ICrudRepository<TestEntity>>();
            repositoryMock.Setup(_ => _.CreateAsync(It.IsAny<TestEntity>())).ReturnsAsync(_entity);
            var service = new CrudService<TestDto, TestEntity>(repositoryMock.Object, _mapper);

            var result = await service.CreateAsync(_dto);

            Assert.Equal(_entity.Id, result.Id);
            repositoryMock.Verify(_ => _.CreateAsync(It.IsAny<TestEntity>()), Times.Once);
            repositoryMock.Verify(_ => _.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Update_ReturnsUpdatedElement()
        {
            var repositoryMock = new Mock<ICrudRepository<TestEntity>>();
            repositoryMock.Setup(_ => _.GetById(_entity.Id)).Returns(_entity);
            var service = new CrudService<TestDto, TestEntity>(repositoryMock.Object, _mapper);

            var result = service.Update(_entity.Id, _dto);

            Assert.Equal(_entity.Id, result.Id);
            repositoryMock.Verify(_ => _.GetById(It.IsAny<Guid>()), Times.Once());
            repositoryMock.Verify(_ => _.SaveChanges(), Times.Once);
        }

        [Fact]
        public async void UpdateAsync_ReturnsUpdatedElement()
        {
            var repositoryMock = new Mock<ICrudRepository<TestEntity>>();
            repositoryMock.Setup(_ => _.GetByIdAsync(_entity.Id)).ReturnsAsync(_entity);
            var service = new CrudService<TestDto, TestEntity>(repositoryMock.Object, _mapper);

            var result = await service.UpdateAsync(_entity.Id, _dto);

            Assert.Equal(_entity.Id, result.Id);
            repositoryMock.Verify(_ => _.GetByIdAsync(It.IsAny<Guid>()), Times.Once());
            repositoryMock.Verify(_ => _.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Update_ThrowsEntityNotFoundException()
        {
            var repositoryMock = new Mock<ICrudRepository<TestEntity>>();
            repositoryMock.Setup(_ => _.GetById(It.IsAny<Guid>())).Throws<EntityNotFoundException>();
            var service = new CrudService<TestDto, TestEntity>(repositoryMock.Object, _mapper);

            Assert.Throws<EntityNotFoundException>(() => service.Update(Guid.Empty, _entity));
        }

        [Fact]
        public async void UpdateAsync_ThrowsEntityNotFoundException()
        {
            var repositoryMock = new Mock<ICrudRepository<TestEntity>>();
            repositoryMock.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ThrowsAsync(new EntityNotFoundException());
            var service = new CrudService<TestDto, TestEntity>(repositoryMock.Object, _mapper);

            await Assert.ThrowsAsync<EntityNotFoundException>(() => service.UpdateAsync(Guid.Empty, _entity));
        }

        [Fact]
        public void Delete_ReturnsDeletedElement()
        {
            var repositoryMock = new Mock<ICrudRepository<TestEntity>>();
            repositoryMock.Setup(_ => _.GetById(_entity.Id)).Returns(_entity);
            var service = new CrudService<TestDto, TestEntity>(repositoryMock.Object, _mapper);

            var result = service.Delete(_entity.Id);

            Assert.Equal(_entity.Id, result.Id);
            repositoryMock.Verify(_ => _.GetById(It.IsAny<Guid>()), Times.Once());
            repositoryMock.Verify(_ => _.Delete(It.IsAny<Guid>()), Times.Once);
            repositoryMock.Verify(_ => _.SaveChanges(), Times.Once);
        }

        [Fact]
        public async void DeleteAsync_ReturnsDeletedElement()
        {
            var repositoryMock = new Mock<ICrudRepository<TestEntity>>();
            repositoryMock.Setup(_ => _.GetByIdAsync(_entity.Id)).ReturnsAsync(_entity);
            var service = new CrudService<TestDto, TestEntity>(repositoryMock.Object, _mapper);

            var result = await service.DeleteAsync(_entity.Id);

            Assert.Equal(_entity.Id, result.Id);
            repositoryMock.Verify(_ => _.GetByIdAsync(It.IsAny<Guid>()), Times.Once());
            repositoryMock.Verify(_ => _.DeleteAsync(It.IsAny<Guid>()), Times.Once);
            repositoryMock.Verify(_ => _.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Delete_ThrowsEntityNotFoundException()
        {
            var repositoryMock = new Mock<ICrudRepository<TestEntity>>();
            repositoryMock.Setup(_ => _.GetById(It.IsAny<Guid>())).Throws<EntityNotFoundException>();
            var service = new CrudService<TestDto, TestEntity>(repositoryMock.Object, _mapper);

            Assert.Throws<EntityNotFoundException>(() => service.Delete(Guid.Empty));
        }

        [Fact]
        public async void DeleteAsync_ThrowsEntityNotFoundException()
        {
            var repositoryMock = new Mock<ICrudRepository<TestEntity>>();
            repositoryMock.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ThrowsAsync(new EntityNotFoundException());
            var service = new CrudService<TestDto, TestEntity>(repositoryMock.Object, _mapper);

            await Assert.ThrowsAsync<EntityNotFoundException>(() => service.DeleteAsync(Guid.Empty));
        }
    }
}
