using System;
using LSG.GenericCrud.Models;

namespace LSG.GenericCrud.Tests.Models
{
    public class TestEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }
}