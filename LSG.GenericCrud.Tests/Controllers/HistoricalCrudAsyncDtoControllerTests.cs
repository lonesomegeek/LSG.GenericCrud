using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Bogus;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Tests.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LSG.GenericCrud.Tests.Controllers
{
    
    public class HistoricalCrudAsyncDtoControllerTests
    {
        private readonly IList<TestEntity> _entities;
        private readonly TestEntity _entity;
        private readonly IMapper _mapper;
        private readonly IList<TestDto> _dtos;
        private readonly TestDto _dto;
        private readonly IList<HistoricalEvent> _events;

        public HistoricalCrudAsyncDtoControllerTests()
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

            var historyFaker = new Faker<HistoricalEvent>();
            _events = historyFaker.Generate(2);

            _mapper = new AutoMapper.MapperConfiguration(_ =>
            {
                _.CreateMap<TestDto, TestEntity>().ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.ValueDto));
                _.CreateMap<TestEntity, TestDto> ().ForMember(dest => dest.ValueDto, opts => opts.MapFrom(src => src.Value));

            }).CreateMapper();
        }

        [Fact]
        public async Task Restore_ReturnsOk()
        {
            var dalMock = new Mock<HistoricalCrud<TestEntity>>();
            dalMock.Setup(_ => _.RestoreAsync(It.IsAny<Guid>())).ReturnsAsync(_entity);
            var controller = new HistoricalCrudAsyncController<TestDto, TestEntity>(dalMock.Object, _mapper);

            var actionResult = await controller.Restore(_entity.Id);

            Assert.IsType<OkObjectResult>(actionResult);
            dalMock.Verify(_ => _.RestoreAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Restore_ReturnsNotFound()
        {
            var dalMock = new Mock<HistoricalCrud<TestEntity>>();
            dalMock.Setup(_ => _.RestoreAsync(It.IsAny<Guid>())).ThrowsAsync(new EntityNotFoundException());
            var controller = new HistoricalCrudAsyncController<TestDto, TestEntity>(dalMock.Object, _mapper);

            var actionResult = await controller.Restore(_entity.Id);

            Assert.IsType<NotFoundResult>(actionResult);
            dalMock.Verify(_ => _.RestoreAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetHistory_ReturnsOk()
        {
            var dalMock = new Mock<HistoricalCrud<TestEntity>>();
            dalMock.Setup(_ => _.GetHistoryAsync(It.IsAny<Guid>())).ReturnsAsync(_events);
            var controller = new HistoricalCrudAsyncController<TestDto, TestEntity>(dalMock.Object, _mapper);

            var actionResult = await controller.GetHistory(_entity.Id);

            Assert.IsType<OkObjectResult>(actionResult);
            dalMock.Verify(_ => _.GetHistoryAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetHistory_ReturnsNotFound()
        {
            var dalMock = new Mock<HistoricalCrud<TestEntity>>();
            dalMock.Setup(_ => _.GetHistoryAsync(It.IsAny<Guid>())).ThrowsAsync(new EntityNotFoundException());
            var controller = new HistoricalCrudAsyncController<TestDto, TestEntity>(dalMock.Object, _mapper);

            var actionResult = await controller.GetHistory(_entity.Id);

            Assert.IsType<NotFoundResult>(actionResult);
            dalMock.Verify(_ => _.GetHistoryAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
