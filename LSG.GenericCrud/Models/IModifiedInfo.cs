using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GenericCrud.Models
{
    public interface IModifiedInfo
    {
        string ModifiedBy { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}
