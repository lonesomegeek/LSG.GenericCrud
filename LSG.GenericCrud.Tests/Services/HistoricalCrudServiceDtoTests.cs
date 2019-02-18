//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using AutoMapper;
//using Bogus;
//using LSG.GenericCrud.Dto.Services;
//using LSG.GenericCrud.Models;
//using LSG.GenericCrud.Repositories;
//using LSG.GenericCrud.Services;
//using LSG.GenericCrud.Tests.Models;
//using Moq;
//using Xunit;

//namespace LSG.GenericCrud.Tests.Services
//{
//    public class HistoricalCrudServiceDtoTests
//    {
//        private readonly IList<TestEntity> _entities;
//        private readonly TestEntity _entity;
//        private readonly List<HistoricalEvent> _events;
//        private readonly IMapper _mapper;
//        private readonly IList<TestDto> _dtos;
//        private readonly TestDto _dto;

//        public HistoricalCrudServiceDtoTests()
//        {
//            Randomizer.Seed = new Random(1234567);
//            var entityFaker = new Faker<TestEntity>().
//                RuleFor(_ => _.Id, Guid.NewGuid()).
//                RuleFor(_ => _.Value, _ => _.Lorem.Word());
//            _entities = entityFaker.Generate(5);
//            _entity = entityFaker.Generate();
//            var eventFaker = new Faker<HistoricalEvent>().
//                RuleFor(_ => _.Id, Guid.NewGuid).
//                RuleFor(_ => _.Action, HistoricalActions.Delete.ToString).
//                RuleFor(_ => _.EntityId, _entity.Id).
//                RuleFor(_ => _.Changeset, "{}");
//            _events = new List<HistoricalEvent>() { eventFaker.Generate() };

//            var dtoFaker = new Faker<TestDto>().
//                RuleFor(_ => _.Id, Guid.NewGuid()).
//                RuleFor(_ => _.ValueDto, _ => _.Lorem.Word());
//            _dtos = dtoFaker.Generate(5);
//            _dto = dtoFaker.Generate();

//            _mapper = new AutoMapper.MapperConfiguration(_ =>
//            {
//                _.CreateMap<TestDto, TestEntity>().ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.ValueDto));
//                _.CreateMap<TestEntity, TestDto>().ForMember(dest => dest.ValueDto, opts => opts.MapFrom(src => src.Value));

//            }).CreateMapper();
//        }

//        [Fact]
//        public void GetAll_ReturnElements()
//        {
//            var repositoryMock = new Mock<CrudRepository>();
//            repositoryMock.Setup(_ => _.GetAllAsync<TestEntity>()).ReturnsAsync(_entities);
//            var crudService = new CrudService<TestEntity>(repositoryMock.Object);
//            var entityService = new HistoricalCrudService<TestEntity>(crudService, repositoryMock.Object);
//            var service = new HistoricalCrudService<TestDto, TestEntity>(entityService, repositoryMock.Object, _mapper);

//            var results = service.GetAll();

//            Assert.Equal(_entities.Count, results.Count());
//            Assert.IsAssignableFrom<IEnumerable<TestDto>>(results);
//        }

//        [Fact]
//        public void GetById_ReturnElements()
//        {
//            var repositoryMock = new Mock<CrudRepository>();
//            repositoryMock.Setup(_ => _.GetByIdAsync<TestEntity>(It.IsAny<Guid>())).ReturnsAsync(_entity);
//            var crudService = new CrudService<TestEntity>(repositoryMock.Object);
//            var entityService = new HistoricalCrudService<TestEntity>(crudService, repositoryMock.Object);
//            var service = new HistoricalCrudService<TestDto, TestEntity>(entityService, repositoryMock.Object, _mapper);

//            var result = service.GetById(_entity.Id);

//            Assert.Equal(_entity.Id, result.Id);
//            Assert.IsAssignableFrom<TestDto>(result);
//        }

//        [Fact]
//        public void Create_ReturnsCreatedElement()
//        {
//            var repositoryMock = new Mock<CrudRepository>();
//            repositoryMock.Setup(_ => _.CreateAsync(It.IsAny<TestEntity>())).ReturnsAsync(_entity);
//            var crudService = new CrudService<TestEntity>(repositoryMock.Object);
//            var entityService = new HistoricalCrudService<TestEntity>(crudService, repositoryMock.Object);
//            var service = new HistoricalCrudService<TestDto, TestEntity>(entityService, repositoryMock.Object, _mapper);

//            var result = service.Create(_dto);

//            Assert.Equal(_entity.Id, result.Id);
//            repositoryMock.Verify(_ => _.CreateAsync(It.IsAny<HistoricalEvent>()), Times.Once);
//            repositoryMock.Verify(_ => _.CreateAsync(It.IsAny<TestEntity>()), Times.Once);
//            repositoryMock.Verify(_ => _.SaveChangesAsync(), Times.Once);
//        }

//        [Fact]
//        public void Update_ReturnsUpdatedElement()
//        {
//            var repositoryMock = new Mock<CrudRepository>();
//            repositoryMock.Setup(_ => _.GetByIdAsync<TestEntity>(It.IsAny<Guid>())).ReturnsAsync(_entity);
//            var crudService = new CrudService<TestEntity>(repositoryMock.Object);
//            var entityService = new HistoricalCrudService<TestEntity>(crudService, repositoryMock.Object);
//            var service = new HistoricalCrudService<TestDto, TestEntity>(entityService, repositoryMock.Object, _mapper);

//            var result = service.Update(_dto.Id, _dto);

//            Assert.Equal(_entity.Id, result.Id);
//            repositoryMock.Verify(_ => _.CreateAsync(It.IsAny<HistoricalEvent>()), Times.Once);
//            repositoryMock.Verify(_ => _.SaveChangesAsync(), Times.Once);
//        }

//        [Fact]
//        public void Delete_ReturnsDeletedElement()
//        {
//            var repositoryMock = new Mock<CrudRepository>();
//            repositoryMock.Setup(_ => _.GetByIdAsync<TestEntity>(It.IsAny<Guid>())).ReturnsAsync(_entity);
//            var crudService = new CrudService<TestEntity>(repositoryMock.Object);
//            var entityService = new HistoricalCrudService<TestEntity>(crudService, repositoryMock.Object);
//            var service = new HistoricalCrudService<TestDto, TestEntity>(entityService, repositoryMock.Object, _mapper);
            
//            var result = service.Delete(_entity.Id);

//            Assert.Equal(_entity.Id, result.Id);
//            repositoryMock.Verify(_ => _.CreateAsync(It.IsAny<HistoricalEvent>()), Times.Once);
//            repositoryMock.Verify(_ => _.DeleteAsync<TestEntity>(It.IsAny<Guid>()), Times.Once);
//            repositoryMock.Verify(_ => _.SaveChangesAsync(), Times.Once);
//        }

//        [Fact]
//        public void Restore_ReturnsCreatedElement()
//        {
//            var repositoryMock = new Mock<CrudRepository>();
//            repositoryMock.Setup(_ => _.GetAllAsync<HistoricalEvent>()).ReturnsAsync(_events);
//            repositoryMock.Setup(_ => _.CreateAsync(It.IsAny<TestEntity>())).ReturnsAsync(_entity);
//            var crudService = new CrudService<TestEntity>(repositoryMock.Object);
//            var entityService = new HistoricalCrudService<TestEntity>(crudService, repositoryMock.Object);
//            var service = new HistoricalCrudService<TestDto, TestEntity>(entityService, repositoryMock.Object, _mapper);

//            var result = service.Restore(_entity.Id);

//            Assert.Equal(_entity.Id, result.Id);
//            repositoryMock.Verify(_ => _.CreateAsync(It.IsAny<TestEntity>()), Times.Once);
//        }

//    }
//}
