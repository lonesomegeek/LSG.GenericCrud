using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Complete.Models
{
    public interface IStatusable
    {
        string Status { get; set; }
    }
}
