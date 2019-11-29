using System;
using System.ComponentModel.DataAnnotations.Schema;
using LSG.GenericCrud.Models;

namespace Sample.GlobalFilters.Models
{
    public class Item : IEntity, ISoftwareDelete, IHardwareDelete
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }
        [NotMapped]
        public bool IsHardwareDelete { get; set; }
    }
}
