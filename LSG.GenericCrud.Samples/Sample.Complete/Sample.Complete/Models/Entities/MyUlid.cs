using LSG.GenericCrud.Models;
using NUlid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Complete.Models.Entities
{
    public class MyUlid : IEntity<string>
    {
        public MyUlid()
        {
            Id = Ulid.NewUlid().ToString();
        }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
