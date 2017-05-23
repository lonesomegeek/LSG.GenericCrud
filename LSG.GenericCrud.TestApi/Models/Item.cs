using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.DAL;

namespace LSG.GenericCrud.TestApi.Models
{
    public class Item : IEntity
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }
}
