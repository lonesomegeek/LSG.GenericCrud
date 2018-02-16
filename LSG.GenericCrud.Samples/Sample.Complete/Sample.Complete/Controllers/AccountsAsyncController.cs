using System;
using System.Linq;
using LSG.GenericCrud.Controllers;
using Microsoft.AspNetCore.Mvc;
using Sample.Complete.Models.Entities;
using LSG.GenericCrud.Services;
using System.Threading.Tasks;

namespace Sample.Complete.Controllers
{
    [Route("api/[controller]")]
    public class AccountsAsyncController : CrudAsyncController<Account>
    {
        private readonly ICrudService<Contact> _contactService;

        public AccountsAsyncController(ICrudService<Account> accountService, ICrudService<Contact> contactService) : base(accountService)
        {
            _contactService = contactService;
        }

        [Route("{accountId}/contacts")]
        public async Task<IActionResult> GetAccountContacts(Guid accountId)
        {
            var accounts = await _contactService.GetAllAsync();
            return Ok(accounts.Where(_ => _.AccountId == accountId));
        }
    }
}
