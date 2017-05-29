using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Models
{
    public class HistoricalEvent : BaseEntity, IEntity
    {
        public Guid Id { get; set; }
        public Guid EntityId { get; set; }
        public string EntityName { get; set; }
        public string Action { get; set; }
        public string Changeset { get; set; }

    }
}
