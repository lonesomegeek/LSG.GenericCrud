﻿using System;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using Sample.DataFiller.Models.Entities;

namespace Sample.DataFiller.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : CrudController<Account>
    {
        public AccountsController(ICrudController<Guid, Account> controller) : base(controller)
        {
        }
    }
}
