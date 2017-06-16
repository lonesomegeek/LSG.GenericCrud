using System;
using LSG.GenericCrud.Models;

namespace LSG.GenericCrud.TestApi.Models.Entities
{
    public class Carrot : BaseEntity, IEntity
    {
        public Carrot()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Color { get; set; }
    }
}
