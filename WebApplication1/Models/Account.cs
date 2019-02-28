using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Helpers;
using LSG.GenericCrud.Models;

namespace WebApplication1.Models
{
    public class Account : 
        BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
