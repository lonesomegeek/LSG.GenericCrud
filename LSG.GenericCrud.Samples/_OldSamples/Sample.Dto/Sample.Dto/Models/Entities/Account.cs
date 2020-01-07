using System;
using LSG.GenericCrud.Models;

namespace Sample.Dto.Models.Entities
{
    public class Account : IEntity
    {
        public Account()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AnnualRevenue { get; set; }
    }
}
