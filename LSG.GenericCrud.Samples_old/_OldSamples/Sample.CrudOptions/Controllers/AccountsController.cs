using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Extensions.Handlers;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using Sample.CrudOptions.Models;

namespace Sample.CrudOptions.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : CrudController<Account>, ICrudAuthorizationOptions
    {

        public CrudAuthorizationOptions Options => new CrudAuthorizationOptionsBuilder()
            .IsCreateAvailable(true)
            .IsReadAvailable(true)
            .IsUpdateAvailable(false)
            .IsDeleteAvailable(false)
            .IsCreateProtected(true) // will search for accounts.create scope
            .IsReadProtected(false)
            .Build();

        public AccountsController(ICrudController<Guid, Account> controller) : base(controller)
        {
        }
    }
}
