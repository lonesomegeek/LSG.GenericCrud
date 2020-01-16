using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;

namespace Sample.GlobalFilters.Models
{
    public class Car : IEntity, ISingleTenantEntity
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }

        public string Model { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
        public string Serial { get; set; }
        public int Mileage { get; set; }

    }
}
