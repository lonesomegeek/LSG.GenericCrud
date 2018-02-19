using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Dto.Services;
using Microsoft.AspNetCore.Mvc;
using Sample.Dto.Models.DTOs;
using Sample.Dto.Models.Entities;

namespace Sample.Dto.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : HistoricalCrudController<AccountDto>
    {
        public AccountsController(HistoricalCrudService<AccountDto, Account> service) : base(service) { }
    }
}
