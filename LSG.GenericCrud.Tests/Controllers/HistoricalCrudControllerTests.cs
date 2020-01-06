using System;
using System.Collections.Generic;
using Bogus;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Services;
using LSG.GenericCrud.Tests.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LSG.GenericCrud.Tests.Controllers
{
    public class HistoricalCrudControllerTests
    {
        private readonly IList<TestEntity> _entities;
        private readonly TestEntity _entity;
        private readonly IList<HistoricalEvent> _events;
        private readonly IList<ReadeableStatus<TestEntity>> _statuses;
        private readonly ReadeableStatus<TestEntity> _status;

        public HistoricalCrudControllerTests()
        {
            Randomizer.Seed = new Random(1234567);
            var entityFaker = new Faker<TestEntity>().
                RuleFor(_ => _.Id, Guid.NewGuid()).
                RuleFor(_ => _.Value, _ => _.Lorem.Word());
            _entities = entityFaker.Generate(5);
            _entity = entityFaker.Generate();
            var historyFaker = new Faker<HistoricalEvent>();
            _events = historyFaker.Generate(2);

            var readeableStatusFaker = new Faker<ReadeableStatus<TestEntity>>();
            _statuses = readeableStatusFaker.Generate(5);
            _status = readeableStatusFaker.Generate();
        }

        [Fact]
        public async void RestoreAsync_ReturnsOk()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.RestoreAsync(It.IsAny<Guid>())).ReturnsAsync(_entity);
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var result = await controller.RestoreFromDeletedEntity(_entity.Id);

            Assert.IsType<OkObjectResult>(result);
            serviceMock.Verify(_ => _.RestoreAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void Restore_ThrowsEntityNotFoundException()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.RestoreAsync(It.IsAny<Guid>())).ThrowsAsync(new EntityNotFoundException());
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var result = await controller.RestoreFromDeletedEntity(_entity.Id);

            Assert.IsType<NotFoundResult>(result);
            serviceMock.Verify(_ => _.RestoreAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void GetHistory_ReturnsOk()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.GetHistoryAsync(It.IsAny<Guid>())).ReturnsAsync(_events);
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var result = await controller.GetHistory(_entity.Id);

            Assert.IsType<OkObjectResult>(result);
            serviceMock.Verify(_ => _.GetHistoryAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void GetHistory_ThrowsEntityNotFoundException()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.GetHistoryAsync(It.IsAny<Guid>())).ThrowsAsync(new EntityNotFoundException());
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);
            
            var result = await controller.GetHistory(_entity.Id);

            Assert.IsType<NotFoundResult>(result);
            serviceMock.Verify(_ => _.GetHistoryAsync(It.IsAny<Guid>()), Times.Once);
        }


        [Fact]
        public async void GetAll_Calls_CrudController_Definition()
        {
            var controllerMock = new Mock<ICrudController<Guid, TestEntity>>();
            controllerMock.Setup(_ => _.GetAll()).ReturnsAsync(new OkObjectResult(_entities));
            var controller = new HistoricalCrudController<Guid, TestEntity>(controllerMock.Object, null);

            var result = await controller.GetAll();

            Assert.IsType<ActionResult<IEnumerable<TestEntity>>>(result);
            controllerMock.Verify(_ => _.GetAll(), Times.Once);
        }

        [Fact]
        public async void GetReadStatus_ReturnsAsyncOk()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.GetReadStatusAsync()).ReturnsAsync(_statuses);
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.GetReadStatus();

            Assert.IsType<ActionResult<IEnumerable<ReadeableStatus<TestEntity>>>>(actionResult);
            serviceMock.Verify(_ => _.GetReadStatusAsync(), Times.Once);
        }

        [Fact]
        public async void GetReadStatusByIdAsync_ReturnsAsyncOk()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.GetReadStatusByIdAsync(It.IsAny<Guid>())).ReturnsAsync(_status);
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.GetReadStatusById(_entity.Id);

            Assert.IsType<ActionResult<ReadeableStatus<TestEntity>>>(actionResult);
            serviceMock.Verify(_ => _.GetReadStatusByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void GetReadStatusByIdAsync_ReturnsAsyncNotFound()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.GetReadStatusByIdAsync(It.IsAny<Guid>())).ThrowsAsync(new EntityNotFoundException());
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.GetReadStatusById(_entity.Id);

            Assert.IsType<NotFoundResult>(actionResult.Result);
            serviceMock.Verify(_ => _.GetReadStatusByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void GetById_Calls_CrudController_Definition()
        {
            var controllerMock = new Mock<ICrudController<Guid, TestEntity>>();
            controllerMock.Setup(_ => _.GetById(It.IsAny<Guid>())).ReturnsAsync(new OkObjectResult(_entity));
            var controller = new HistoricalCrudController<Guid, TestEntity>(controllerMock.Object, null);

            var result = await controller.GetById(_entity.Id);

            Assert.IsType<ActionResult<TestEntity>>(result);
            controllerMock.Verify(_ => _.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void Head_Calls_CrudController_Definition()
        {
            var controllerMock = new Mock<ICrudController<Guid, TestEntity>>();
            controllerMock.Setup(_ => _.HeadById(It.IsAny<Guid>())).ReturnsAsync(new OkResult());
            var controller = new HistoricalCrudController<Guid, TestEntity>(controllerMock.Object, null);

            await controller.HeadById(_entity.Id);

            controllerMock.Verify(_ => _.HeadById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void Create_ReturnsAsyncCreatedEntity()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.CreateAsync(It.IsAny<TestEntity>())).ReturnsAsync(_entity);
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.Create(_entity);

            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            serviceMock.Verify(_ => _.CreateAsync(It.IsAny<TestEntity>()), Times.Once);
        }

        [Fact]
        public async void Update_ReturnsAsyncModifiedEntity()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.Update(_entity.Id, _entity);

            Assert.IsType<NoContentResult>(actionResult);
            serviceMock.Verify(_ => _.UpdateAsync(It.IsAny<Guid>(), It.IsAny<TestEntity>()), Times.Once);
        }

        [Fact]
        public async void Update_ReturnsAsyncNotFound()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.UpdateAsync(It.IsAny<Guid>(), It.IsAny<TestEntity>())).Throws<EntityNotFoundException>();
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.Update(_entity.Id, _entity);

            Assert.IsType<NotFoundResult>(actionResult);
            serviceMock.Verify(_ => _.UpdateAsync(It.IsAny<Guid>(), It.IsAny<TestEntity>()), Times.Once);
        }

        [Fact]
        public async void Delete_ReturnsAsyncOk()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.Delete(_entity.Id);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            serviceMock.Verify(_ => _.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void Delete_ReturnsAsyncNotFound()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.DeleteAsync(It.IsAny<Guid>())).Throws<EntityNotFoundException>();
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.Delete(_entity.Id);

            Assert.IsType<NotFoundResult>(actionResult.Result);
            serviceMock.Verify(_ => _.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void Copy_ReturnsNotFound()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.CopyAsync(It.IsAny<Guid>())).ThrowsAsync(new EntityNotFoundException());
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.Copy(_entity.Id);

            Assert.IsType<NotFoundResult>(actionResult.Result);
            serviceMock.Verify(_ => _.CopyAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void Copy_ReturnsOk()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.CopyAsync(It.IsAny<Guid>())).ReturnsAsync(_entity);
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.Copy(_entity.Id);

            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            serviceMock.Verify(_ => _.CopyAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void CopyFromChangeset_ReturnsOk()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.CopyFromChangeset(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(_entity);
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.CopyFromChangeset(_entity.Id, new Guid());

            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            serviceMock.Verify(_ => _.CopyFromChangeset(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);

        }

        [Fact]
        public async void CopyFromChangeset_ReturnsNotFound_For_EntityNotFoundException()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.CopyFromChangeset(It.IsAny<Guid>(), It.IsAny<Guid>())).ThrowsAsync(new EntityNotFoundException());
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.CopyFromChangeset(_entity.Id, new Guid());

            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            serviceMock.Verify(_ => _.CopyFromChangeset(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void CopyFromChangeset_ReturnsNotFound_For_ChangesetNotFoundException()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.CopyFromChangeset(It.IsAny<Guid>(), It.IsAny<Guid>())).ThrowsAsync(new ChangesetNotFoundException());
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.CopyFromChangeset(_entity.Id, new Guid());

            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            serviceMock.Verify(_ => _.CopyFromChangeset(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void CopyFromChangeset_ReturnsBadRequest_For_UnmanageableException()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.CopyFromChangeset(It.IsAny<Guid>(), It.IsAny<Guid>())).ThrowsAsync(new Exception());
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.CopyFromChangeset(_entity.Id, new Guid());

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            serviceMock.Verify(_ => _.CopyFromChangeset(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void RestoreFromChangeset_ReturnsNotFound_For_EntityNotFoundException()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.RestoreFromChangeset(It.IsAny<Guid>(), It.IsAny<Guid>())).ThrowsAsync(new EntityNotFoundException());
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.RestoreFromChangeset(_entity.Id, new Guid());

            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            serviceMock.Verify(_ => _.RestoreFromChangeset(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);

        }

        [Fact]
        public async void RestoreFromChangeset_ReturnsNotFound_For_ChangesetNotFoundException()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.RestoreFromChangeset(It.IsAny<Guid>(), It.IsAny<Guid>())).ThrowsAsync(new ChangesetNotFoundException());
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.RestoreFromChangeset(_entity.Id, new Guid());

            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            serviceMock.Verify(_ => _.RestoreFromChangeset(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void RestoreFromChangeset_ReturnsBadRequest_For_UnmanageableException()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.RestoreFromChangeset(It.IsAny<Guid>(), It.IsAny<Guid>())).ThrowsAsync(new Exception());
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.RestoreFromChangeset(_entity.Id, new Guid());

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            serviceMock.Verify(_ => _.RestoreFromChangeset(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void RestoreFromChangeset_ReturnsNoContent()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.RestoreFromChangeset(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(_entity);
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.RestoreFromChangeset(_entity.Id, new Guid());

            Assert.IsType<NoContentResult>(actionResult.Result);
            serviceMock.Verify(_ => _.RestoreFromChangeset(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);

        }

        [Fact]
        public async void MarkAllAsRead_ReturnsNoContent()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.MarkAllAsRead();

            Assert.IsType<NoContentResult>(actionResult);
            serviceMock.Verify(_ => _.MarkAllAsRead(), Times.Once);
        }

        [Fact]
        public async void MarkAllAsUnread_ReturnsNoContent()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.MarkAllAsUnread();

            Assert.IsType<NoContentResult>(actionResult);
            serviceMock.Verify(_ => _.MarkAllAsUnread(), Times.Once);
        }

        [Fact]
        public async void MarkOneAsRead_ReturnsOk()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.MarkOneAsRead(_entity.Id);

            Assert.IsType<NoContentResult>(actionResult);
            serviceMock.Verify(_ => _.MarkOneAsRead(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void MarkOneAsRead_ReturnsNotFound()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.MarkOneAsRead(It.IsAny<Guid>())).ThrowsAsync(new EntityNotFoundException());
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.MarkOneAsRead(_entity.Id);

            Assert.IsType<NotFoundResult>(actionResult);
            serviceMock.Verify(_ => _.MarkOneAsRead(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void MarkOneAsUnread_ReturnsOk()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.MarkOneAsUnread(_entity.Id);

            Assert.IsType<NoContentResult>(actionResult);
            serviceMock.Verify(_ => _.MarkOneAsUnread(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void MarkOneAsUnread_ReturnsNotFound()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.MarkOneAsUnread(It.IsAny<Guid>())).ThrowsAsync(new EntityNotFoundException());
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.MarkOneAsUnread(_entity.Id);

            Assert.IsType<NotFoundResult>(actionResult);
            serviceMock.Verify(_ => _.MarkOneAsUnread(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void Delta_ReturnsOk()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.Delta(It.IsAny<Guid>(), It.IsAny<DeltaRequest>())).ReturnsAsync(new { });
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.Delta(_entity.Id, new DeltaRequest());

            Assert.IsType<OkObjectResult>(actionResult);
            serviceMock.Verify(_ => _.Delta(It.IsAny<Guid>(), It.IsAny<DeltaRequest>()), Times.Once);
        }

        [Fact]
        public async void Delta_ReturnsNotFound()
        {
            var serviceMock = new Mock<IHistoricalCrudService<Guid, TestEntity>>();
            serviceMock.Setup(_ => _.Delta(It.IsAny<Guid>(), It.IsAny<DeltaRequest>())).ThrowsAsync(new EntityNotFoundException());
            var controller = new HistoricalCrudController<Guid, TestEntity>(null, serviceMock.Object);

            var actionResult = await controller.Delta(_entity.Id, new DeltaRequest());

            Assert.IsType<NotFoundResult>(actionResult);
            serviceMock.Verify(_ => _.Delta(It.IsAny<Guid>(), It.IsAny<DeltaRequest>()), Times.Once);
        }

    }
}
