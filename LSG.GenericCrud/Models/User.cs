using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GenericCrud.Models
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
