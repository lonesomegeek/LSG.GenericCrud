using System;
using System.Collections.Generic;
using System.Linq;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Repositories.DataFillers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace LSG.GenericCrud.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void UpdateEntityWithNullValue_Success()
        {
            var entity = new TestEntity() {Id = Guid.NewGuid()};
            var options = new DbContextOptionsBuilder<TestContext>().UseInMemoryDatabase(databaseName: "tests").Options;
            var context = new TestContext(options, null);
            var dal = new HistoricalCrud<TestEntity>(context);
            dal.Create(entity);            
            dal.Update(entity.Id, entity);

            // assert for create and update event
            Assert.Equal(2, context.HistoricalEvents.Count());
        }

        [Fact]
        public void CreateChangesetWithNullValue_Success()
        {
            var originalEntity = new TestEntity() {Id = Guid.NewGuid()}; // create a test entity with a null value
            var modifiedEntity = new TestEntity() {Id = originalEntity.Id, Value = "New Value"};

            var changeset = originalEntity.DetailedCompare(modifiedEntity);

            Assert.Contains("New Value", changeset);
        }
    }
}
