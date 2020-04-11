
using System;
using LSG.GenericCrud.Models;

namespace LSG.GenericCrud.Samples.Models.Entities
{
    public class Contact : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get;set; }
    }
}