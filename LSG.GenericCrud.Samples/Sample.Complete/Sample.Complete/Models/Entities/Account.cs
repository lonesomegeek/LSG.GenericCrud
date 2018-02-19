using System;
using LSG.GenericCrud.Models;

namespace Sample.Complete.Models.Entities
{
    public class Account : IEntity
    {
        public Account()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
