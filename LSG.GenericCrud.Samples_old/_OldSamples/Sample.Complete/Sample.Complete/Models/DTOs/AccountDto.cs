using System;
using LSG.GenericCrud.Models;

namespace Sample.Complete.Models.DTOs
{
    public class AccountDto : IEntity
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
    }
}
