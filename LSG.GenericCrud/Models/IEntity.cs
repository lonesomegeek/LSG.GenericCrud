using System;

namespace LSG.GenericCrud.Models
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
