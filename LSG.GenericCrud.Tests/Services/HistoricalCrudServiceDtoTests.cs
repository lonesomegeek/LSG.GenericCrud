using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Bogus;
using LSG.GenericCrud.Dto.Services;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using LSG.GenericCrud.Tests.Models;
using Moq;
using Xunit;

namespace LSG.GenericCrud.Tests.Services
{
    public class HistoricalCrudServiceDtoTests
    {
        private readonly IList<TestEntity> _entities;
        private readonly TestEntity _entity;
        private readonly List<HistoricalEvent> _events;
        private readonly IMapper _mapper;
        private readonly IList<TestDto> _dtos;
        private readonly TestDto _dto;

        public HistoricalCrudServiceDtoTests()
        {
            Randomizer.Seed = new Random(1234567);
            var entityFaker = new Faker<TestEntity>().
                RuleFor(_ => _.Id, Guid.NewGuid()).
                RuleFor(_ => _.Value, _ => _.Lorem.Word());
            _entities = entityFaker.Generate(5);
            _entity = entityFaker.Generate();
            var eventFaker = new Faker<HistoricalEvent>().
                RuleFor(_ => _.Id, Guid.NewGuid).
                RuleFor(_ => _.Action, HistoricalActions.Delete.ToString).
                RuleFor(_ => _.EntityId, _entity.Id).
                RuleFor(_ => _.Changeset, "{}");
            _events = new List<HistoricalEvent>() { eventFaker.Generate() };

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
        public void Create_ReturnsCreatedElement()
        {
            var eventRepositoryMock = new Mock<CrudRepository<HistoricalEvent>>();
            var entityRepositoryMock = new Mock<CrudRepository<TestEntity>>();
            entityRepositoryMock.Setup(_ => _.Create(It.IsAny<TestEntity>())).Returns(_entity);
            var service = new HistoricalCrudService<TestDto, TestEntity>(entityRepositoryMock.Object, eventRepositoryMock.Object, _mapper);

            var result = service.Create(_entity);

            Assert.Equal(_entity.Id, result.Id);
            eventRepositoryMock.Verify(_ => _.Create(It.IsAny<HistoricalEvent>()), Times.Once);
            entityRepositoryMock.Verify(_ => _.Create(It.IsAny<TestEntity>()), Times.Once);
            entityRepositoryMock.Verify(_ => _.SaveChanges(), Times.Once);
            eventRepositoryMock.Verify(_ => _.SaveChanges(), Times.Once);
        }
    }
}
