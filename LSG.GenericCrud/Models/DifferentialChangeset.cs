using System;
using System.Collections.Generic;
using LSG.GenericCrud.Controllers;

namespace LSG.GenericCrud.Models
{
    public class DifferentialChangeset
    {
        public string EntityTypeName { get; set; }
        public string EntityId { get; set; }
        public List<Changeset> Changesets { get; set; }
    }
}