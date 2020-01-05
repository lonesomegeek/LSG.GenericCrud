using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using Sample.Models;
using System;

namespace Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : CrudController<Account>
    {
        public AccountsController(ICrudController<Guid, Account> controller) : base(controller)
        {
        }
    }
}