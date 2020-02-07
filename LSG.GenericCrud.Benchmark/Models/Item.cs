using LSG.GenericCrud.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GenericCrud.Benchmark.Models
{
    public class Item :
        IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
