using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.TestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LSG.GenericCrud.TestApi.Controllers
{
    [Route("api/[controller]")]
    public class ItemsTestController : Controller
    {
        private readonly MyContext _context;

        public ItemsTestController(MyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_context.Items);
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetById(Guid id) => Ok(_context.Items.SingleOrDefault(_ => _.Id == id));

        [HttpPost]
        public IActionResult Create()
        {
            _context.Items.Add(new Item() { Value = DateTime.Now.ToString() });
            _context.SaveChanges();
            return Ok();
        }
    }
}
