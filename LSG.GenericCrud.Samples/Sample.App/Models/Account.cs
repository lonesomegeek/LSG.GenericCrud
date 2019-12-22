using LSG.GenericCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.App.Models
{
    public class Account : BaseEntity, IEntity<Guid>
    {
        public string Name { get; set; }
    }
}
