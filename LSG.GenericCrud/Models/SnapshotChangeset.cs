using System;
using System.Collections.Generic;
using LSG.GenericCrud.Controllers;

namespace LSG.GenericCrud.Models
{
    public class SnapshotChangeset
    {

        public string EntityTypeName { get; set; }
        public string EntityId { get; set; }
        public DateTime LastViewed { get; set; }
        public List<Change> Changes { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedEvent { get; set; }
    }
}