using LSG.GenericCrud.Helpers;
using LSG.GenericCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Samples.Models
{
    public class Item : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get;set; }
    }
}