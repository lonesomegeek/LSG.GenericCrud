using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;

namespace Sample.HistoricalCrud.Models
{
    public class AccountDto : IEntity
    {
        public AccountDto()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string FullName { get; set; }
    }
}
