using System;
using System.Collections.Generic;
using System.Text;
using LSG.GenericCrud.Tests.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LSG.GenericCrud.Tests.Repositories
{
    public class TestSet : DbSet<TestEntity>
    {
        public TestSet()
        {
            
        }
        public override LocalView<TestEntity> Local { get; }
    }
}
