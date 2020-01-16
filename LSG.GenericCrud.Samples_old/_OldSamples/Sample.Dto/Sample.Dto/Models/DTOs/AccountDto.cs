using System;
using LSG.GenericCrud.Models;

namespace Sample.Dto.Models.DTOs
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
