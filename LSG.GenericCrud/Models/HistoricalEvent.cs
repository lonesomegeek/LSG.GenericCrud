using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="LSG.GenericCrud.Models.BaseEntity" />
    /// <seealso cref="LSG.GenericCrud.Models.IEntity" />
    public class HistoricalEvent : 
        IEntity,
        ICreatedInfo
    {
        public Guid Id { get; set; }
        public HistoricalChangeset Changeset { get; set; }
        public string EntityId { get; set; }
        public string EntityName { get; set; }
        public string Action { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
