using LSG.GenericCrud.Models;
using System;

namespace LSG.GenericCrud.Samples.OData.Models
{
    public class Account : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
