using System;
using LSG.GenericCrud.Models;

namespace Sample.Complete.Models.Entities
{
    public class Account : 
        IEntity,
        IStatusable
    {
        public Account()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
