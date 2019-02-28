using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    internal class SnapshotChangeset
    {

        public string EntityTypeName { get; internal set; }
        public Guid EntityId { get; internal set; }
        public DateTime LastViewed { get; internal set; }
        public List<Change> Changes { get; internal set; }
        public DateTime LastModifiedDate { get; internal set; }
        public string LastModifiedBy { get; internal set; }
        public string LastModifiedEvent { get; internal set; }
    }


}
