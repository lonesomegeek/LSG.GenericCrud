using System;
using LSG.GenericCrud.Models;

namespace LSG.GenericCrud.TestApi.Models
{
    public class Carrot : IEntity
    {
        public Carrot()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Color { get; set; }
    }
}
