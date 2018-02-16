using System;
using LSG.GenericCrud.Models;

namespace Sample.HistoricalCrud.Models
{
    public class AccountModel : IEntity
    {
        public AccountModel()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AnnualRevenue { get; set; }
    }
}
