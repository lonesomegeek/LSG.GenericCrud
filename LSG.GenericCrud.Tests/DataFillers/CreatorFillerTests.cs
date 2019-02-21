using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LSG.GenericCrud.DataFillers;
using LSG.GenericCrud.Tests.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Moq;
using Xunit;

namespace LSG.GenericCrud.Tests.DataFillers
{
    public class CreatorFillerTests
    {
        [Fact]
        public async Task CreatorFiller_Null_NotSupported()
        {
            //var serviceProviderMock = new Mock<IServiceProvider>();
            //var entityMock = new Mock<EntityEntry<TestEntity>>();
            //var filler = new CreatorFiller(serviceProviderMock.Object);
            //var entry = new EntityEntry<TestEntity>(new InternalShadowEntityEntry());
            //var result = filler.IsEntitySupported(entityMock.Object);

            //Assert.False(result);
        }
    }
    
}
