using System;
using LSG.GenericCrud.Models;

namespace LSG.GenericCrud.Samples.Models.DTOs
{
    public class ItemDto : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get;set; }
        public string Color { get;set; }
    }
}