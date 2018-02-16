using System;
using LSG.GenericCrud.Models;

namespace Sample.DataFiller.Models.Entities
{
    public class Account : BaseEntity, IEntity
    {
        public Account()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}