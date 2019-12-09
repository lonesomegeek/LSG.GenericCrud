using LSG.GenericCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Complete.Models.Entities
{
    public class Item : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
