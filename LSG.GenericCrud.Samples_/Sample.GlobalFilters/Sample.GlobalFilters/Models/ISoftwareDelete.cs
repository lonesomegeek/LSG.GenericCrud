using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;

namespace Sample.GlobalFilters.Models
{
    public interface ISoftwareDelete
    {
        bool IsDeleted { get; set; }
    }
}
