using LSG.GenericCrud.Models;
using System;

namespace LSG.GenericCrud.Samples.Models.Entities
{
    public class Item : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string JsonMetadata { get;set; }
    }
}