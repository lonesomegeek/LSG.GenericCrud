using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Controllers;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController :
        ICrudController<Guid, Account>,
        ICrudCopyController<Guid, Account>
    {
        private readonly ICrudController<Guid, Account> _controller;
        private readonly ICrudCopyController<Guid, Account> _copyController;

        public AccountsController(
            ICrudController<Guid, Account> controller,
            ICrudCopyController<Guid, Account> copyController)
        {
            _controller = controller;
            _copyController = copyController;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<Account>>> GetAll() => await _controller.GetAll();
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<Account>> GetById(Guid id) => await _controller.GetById(id);
        [HttpHead("{id}")]
        public virtual async Task<IActionResult> HeadById(Guid id) => await _controller.HeadById(id);
        [HttpPost]
        public virtual async Task<ActionResult<Account>> Create(Account entity) => await _controller.Create(entity);
        [HttpPost("{id}/copy")]
        public virtual async Task<ActionResult<Account>> Copy(Guid id) => await _copyController.Copy(id);
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(Guid id, Account entity) => await _controller.Update(id, entity);
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<Account>> Delete(Guid id) => await _controller.Delete(id);
    }
}
