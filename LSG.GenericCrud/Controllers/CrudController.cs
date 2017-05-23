using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LSG.GenericCrud.Controllers
{
    public class CrudController<T> : Controller where T : class/*, IEntity*/, new()
    {
        private IDbContext _context;

        public CrudController(IDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll() => Ok(_context.Set<T>().ToList());
    }
}
