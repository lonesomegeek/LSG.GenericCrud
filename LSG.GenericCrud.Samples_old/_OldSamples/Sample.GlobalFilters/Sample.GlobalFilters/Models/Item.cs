using System;
using LSG.GenericCrud.Models;

namespace Sample.GlobalFilters.Models
{
    public class Item : IEntity, ISoftwareDelete
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }
    }
}
