using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;

namespace LSG.GenericCrud.TestApi.Models.DTOs
{
    public class CarrotDto : IEntity
    {
        public CarrotDto()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Colorification { get; set; }
    }
}
