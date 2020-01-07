using LSG.GenericCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.App.Models
{
    public class Item : BaseEntity, IEntity<Guid>
    {
        public new Guid Id { get; set; }
        public string Name { get; set; }
    }
}
