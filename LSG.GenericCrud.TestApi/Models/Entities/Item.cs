using System;
using LSG.GenericCrud.Models;

namespace LSG.GenericCrud.TestApi.Models.Entities
{
    public class Item : BaseEntity, IEntity
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }
}
