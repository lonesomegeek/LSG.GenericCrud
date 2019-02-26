using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GenericCrud.Models
{
    public interface ICreatedInfo
    {
        string CreatedBy { get; set; }
        DateTime? CreatedDate { get; set; }
    }
}
