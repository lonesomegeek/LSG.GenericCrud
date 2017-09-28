using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Tests.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LSG.GenericCrud.Tests.Controllers
{
    
    public class HistoricalCrudDtoControllerTests
    {
        private readonly IList<TestDto> _dtos;
        private readonly TestDto _dto;

        public HistoricalCrudDtoControllerTests()
        {
            Randomizer.Seed = new Random(1234567);
            var dtoFaker = new Faker<TestDto>().
                RuleFor(_ => _.Id, Guid.NewGuid()).
                RuleFor(_ => _.ValueDto, _ => _.Lorem.Word());
            _dtos = dtoFaker.Generate(5);
            _dto = dtoFaker.Generate();
        }

        [Fact]
        public void GetAll_ReturnsOk()
        {
            var dalMock = new Mock<HistoricalCrud<TestDto>>();
            dalMock.Setup(_ => _.GetAll()).Returns(_dtos);
            var controller = new HistoricalCrudController<TestDto>(dalMock.Object);

            var actionResult = controller.GetAll();
            var okResult = actionResult as OkObjectResult;
            var model = okResult.Value as IEnumerable<TestDto>;

            Assert.Equal(model.Count(), _dtos.Count);
            dalMock.Verify(_ => _.GetAll(), Times.Once);
        }

        [Fact]
        public void GetById_ReturnsOk()
        {
            var id = _dtos[0].Id;
            var dalMock = new Mock<HistoricalCrud<TestDto>>();
            dalMock.Setup(_ => _.GetById(id)).Returns(_dtos[0]);
            var controller = new HistoricalCrudController<TestDto>(dalMock.Object);
            
            var actionResult = controller.GetById(id);
            var okResult = actionResult as OkObjectResult;
            var model = okResult.Value as TestDto;

            Assert.Equal(model.Id, id);
            dalMock.Verify(_ => _.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void GetById_ReturnsNotFound()
        {
            var dalMock = new Mock<HistoricalCrud<TestDto>>();
            dalMock.Setup(_ => _.GetById(It.IsAny<Guid>())).Throws(new EntityNotFoundException());
            var controller = new HistoricalCrudController<TestDto>(dalMock.Object);
            
            var actionResult = controller.GetById(Guid.NewGuid());
            
            Assert.IsType(typeof(NotFoundResult), actionResult);
            dalMock.Verify(_ => _.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void Create_ReturnsCreatedEntity()
        {
            var dalMock = new Mock<HistoricalCrud<TestDto>>();
            var controller = new HistoricalCrudController<TestDto>(dalMock.Object);

            var actionResult = controller.Create(_dto);

            Assert.IsType<OkObjectResult>(actionResult);
            dalMock.Verify(_ => _.Create(It.IsAny<TestDto>()), Times.Once);
        }

        [Fact]
        public void Update_ReturnsModifiedEntity()
        {
            var dalMock = new Mock<HistoricalCrud<TestDto>>();
            var controller = new HistoricalCrudController<TestDto>(dalMock.Object);

            var actionResult = controller.Update(_dto.Id, _dto);

            Assert.IsType<OkResult>(actionResult);
            dalMock.Verify(_ => _.Update(It.IsAny<Guid>(), It.IsAny<TestDto>()), Times.Once);
        }

        [Fact]
        public void Update_ReturnsNotFound()
        {
            var dalMock = new Mock<HistoricalCrud<TestDto>>();
            dalMock.Setup(_ => _.Update(It.IsAny<Guid>(), It.IsAny<TestDto>())).Throws<EntityNotFoundException>();
            var controller = new HistoricalCrudController<TestDto>(dalMock.Object);

            var actionResult = controller.Update(_dto.Id, _dto);

            Assert.IsType(typeof(NotFoundResult), actionResult);
            dalMock.Verify(_ => _.Update(It.IsAny<Guid>(), It.IsAny<TestDto>()), Times.Once);
        }

        [Fact]
        public void Delete_ReturnsOk()
        {
            var dalMock = new Mock<HistoricalCrud<TestDto>>();
            var controller = new HistoricalCrudController<TestDto>(dalMock.Object);

            var actionResult = controller.Delete(_dto.Id);

            Assert.IsType(typeof(OkResult), actionResult);
            dalMock.Verify(_ => _.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void Delete_ReturnsNotFound()
        {
            var dalMock = new Mock<HistoricalCrud<TestDto>>();
            dalMock.Setup(_ => _.Delete(It.IsAny<Guid>())).Throws<EntityNotFoundException>();
            var controller = new HistoricalCrudController<TestDto>(dalMock.Object);

            var actionResult = controller.Delete(_dto.Id);

            Assert.IsType(typeof(NotFoundResult), actionResult);
            dalMock.Verify(_ => _.Delete(It.IsAny<Guid>()), Times.Once);
        }
    }
}
