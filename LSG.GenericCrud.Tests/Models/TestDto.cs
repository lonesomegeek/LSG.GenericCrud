using System;
using System.Collections.Generic;
using System.Text;
using LSG.GenericCrud.Models;

namespace LSG.GenericCrud.Tests.Models
{
    public class TestDto : IEntity
    {
        public Guid Id { get; set; }
        public string ValueDto { get; set; }
    }
}
