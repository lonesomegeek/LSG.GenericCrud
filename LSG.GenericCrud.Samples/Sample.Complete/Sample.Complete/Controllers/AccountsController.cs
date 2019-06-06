using System;
using System.Linq;
using LSG.GenericCrud.Controllers;
using Microsoft.AspNetCore.Mvc;
using Sample.Complete.Models.Entities;
using LSG.GenericCrud.Services;

namespace Sample.Complete.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : CrudController<Guid, Account>
    {
        private readonly ICrudService<Guid, Contact> _contactService;

        public AccountsController(ICrudService<Guid, Account> service, ICrudService<Guid, Contact> contactService) : base(service)
        {
            _contactService = contactService;
        }

        [Route("{accountId}/contacts")]
        public IActionResult GetAccountContacts(Guid accountId)
        {
            return Ok(_contactService.GetAll().Where(_ => _.AccountId == accountId));
        }

    }
}
