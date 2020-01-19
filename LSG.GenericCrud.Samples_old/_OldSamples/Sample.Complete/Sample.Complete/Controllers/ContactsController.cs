﻿using System;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using Sample.Complete.Models.Entities;

namespace Sample.Complete.Controllers
{
    [Route("api/[controller]")]
    public class ContactsController : CrudController<Contact>
    {
        public ContactsController(ICrudController<Guid, Contact> controller) : base(controller)
        {
        }
    }
}
