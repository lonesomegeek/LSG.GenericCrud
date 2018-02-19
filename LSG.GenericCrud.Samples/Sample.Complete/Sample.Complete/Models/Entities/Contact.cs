using System;
using LSG.GenericCrud.Models;

namespace Sample.Complete.Models.Entities
{
    public class Contact : IEntity
    {
        public Contact()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AccountId { get; set; }
    }
}
