using LSG.GenericCrud.Helpers;
using LSG.GenericCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Samples.Models
{
    public class Item : 
        IEntity<Guid>,
        ICreatedInfo,
        IModifiedInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [IgnoreInChangeset]
        public string ModifiedBy { get; set; }
        [IgnoreInChangeset]
        public DateTime? ModifiedDate { get; set; }
        [IgnoreInChangeset]
        public string CreatedBy { get; set; }
        [IgnoreInChangeset]
        public DateTime? CreatedDate { get; set; }
    }
}
