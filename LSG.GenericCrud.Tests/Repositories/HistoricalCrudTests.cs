using System;
using System.Collections.Generic;
using System.Linq;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Tests.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace LSG.GenericCrud.Tests.Repositories
{
    //public class HistoricalCrudTests
    //{
    //    [Fact]
    //    public void UpdateEntityWithNullValue_Success()
    //    {
    //        var entity = new TestEntity() { Id = Guid.NewGuid() };
    //        var options = new DbContextOptionsBuilder<TestContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
    //        var context = new TestContext(options, null);
    //        var dal = new HistoricalCrud<TestEntity>(context);
    //        dal.Create(entity);

    //        dal.Update(entity.Id, entity);

    //        // assert for create and update event
    //        Assert.Equal(2, context.HistoricalEvents.Count());
    //    }
    //    [Fact]
    //    public void CreateEntityWithId_InitializeIdInDal_Success()
    //    {
    //        var entity = new TestEntity() { Id = Guid.NewGuid() };
    //        var options = new DbContextOptionsBuilder<TestContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
    //        var context = new TestContext(options, null);
    //        var dal = new HistoricalCrud<TestEntity>(context);

    //        dal.Create(entity);

    //        // assert for create and update event
    //        Assert.Equal(1, context.HistoricalEvents.Count());
    //        Assert.Equal(1, context.TestEntities.Count());
    //        Assert.Equal(entity.Id, context.HistoricalEvents.First().EntityId);
    //        Assert.Equal(context.TestEntities.First().Id, context.HistoricalEvents.First().EntityId);
    //    }

    //    [Fact]
    //    public void CreateEntityWithoutIdInitializer_InitializeIdInDal_Success()
    //    {
    //        var entity = new TestEntity();
    //        var options = new DbContextOptionsBuilder<TestContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
    //        var context = new TestContext(options, null);
    //        var dal = new HistoricalCrud<TestEntity>(context);

    //        var createdEntity = dal.Create(entity);

    //        // assert for create and update event
    //        Assert.Equal(1, context.HistoricalEvents.Count());
    //        Assert.Equal(1, context.TestEntities.Count());
    //        Assert.Equal(createdEntity.Id, context.HistoricalEvents.First().EntityId);
    //        Assert.Equal(context.TestEntities.First().Id, context.HistoricalEvents.First().EntityId);
    //    }

    //    [Fact]
    //    public void CreateChangesetWithNullValue_Success()
    //    {
    //        var originalEntity = new TestEntity() { Id = Guid.NewGuid() }; // create a test entity with a null value
    //        var modifiedEntity = new TestEntity() { Id = originalEntity.Id, Value = "New Value" };

    //        var changeset = originalEntity.DetailedCompare(modifiedEntity);

    //        Assert.Contains("New Value", changeset);
    //    }

    //    [Fact]
    //    public void CreateChangesetWithValue_Success()
    //    {
    //        var originalEntity = new TestEntity() { Id = Guid.NewGuid(), Value = "Initial Value" };
    //        var modifiedEntity = new TestEntity() { Id = originalEntity.Id, Value = "New Value" };

    //        var changeset = originalEntity.DetailedCompare(modifiedEntity);

    //        Assert.Contains("New Value", changeset);
    //    }
    //}
}
