using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class DifferentialChangeset
    {
        public string EntityTypeName { get; set; }
        public Guid EntityId { get; set; }
        public List<Changeset> Changesets { get; set; }
    }


}
