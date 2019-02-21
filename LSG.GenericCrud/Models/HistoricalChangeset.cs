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
    public class HistoricalChangeset : 
        IEntity<Guid>,
        ICreatedInfo
    {
        public Guid Id { get; set; }
        public HistoricalEvent Event { get; set; }
        public Guid EventId { get; set; }
        /// <summary>
        /// Receives the information of the actual object before being modified
        /// </summary>
        public string ObjectData { get; set; }
        /// <summary>
        /// Receives the information of the delta that will be applied to the original object
        /// </summary>
        public string ObjectDelta { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
