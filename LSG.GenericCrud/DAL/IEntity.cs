using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSG.GenericCrud.DAL
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
