using LSG.GenericCrud.Helpers;
using LSG.GenericCrud.Models;
using System;

namespace LSG.GenericCrud.Samples.Models.Entities
{
    public class User : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
