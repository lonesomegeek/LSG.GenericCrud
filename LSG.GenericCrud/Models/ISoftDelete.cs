using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GenericCrud.Models
{
    public interface ISoftDelete
    {
        bool? IsDeleted { get; set; }
    }
}
