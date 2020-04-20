using LSG.GenericCrud.Helpers;
using LSG.GenericCrud.Models;
using System;

namespace LSG.GenericCrud.Samples.Models.DTOs
{
    public class UserDto : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
