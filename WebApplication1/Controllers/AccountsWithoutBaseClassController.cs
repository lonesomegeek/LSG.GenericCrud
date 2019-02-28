//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using LSG.GenericCrud.Controllers;
//using Microsoft.AspNetCore.Mvc;
//using WebApplication1.Models;

//namespace WebApplication1.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AccountsWithoutBaseClassController : 
//        ControllerBase,
//        ICrudController<Account>,
//        IHistoricalCrudController<Account>
//    {
//        private readonly ICrudController<Account> _controller;
//        private readonly IHistoricalCrudController<Account> _historicalCrudCrudController;

//        public AccountsWithoutBaseClassController(
//            ICrudController<Account> controller,
//            IHistoricalCrudController<Account> historicalCrudCrudController)
//        {
//            _controller = controller;
//            _historicalCrudCrudController = historicalCrudCrudController;
//        }

//        [HttpPost]
//        public Task<ActionResult<Account>> Create([FromBody] Account entity) => _controller.Create(entity);
//        [HttpDelete("{id}")]
//        public Task<ActionResult<Account>> Delete(Guid id) => _controller.Delete(id);
//        [HttpGet]
//        public Task<ActionResult<IEnumerable<Account>>> GetAll() => _controller.GetAll();
//        [Route("{id}")]
//        [HttpGet]
//        public Task<ActionResult<Account>> GetById(Guid id) => _controller.GetById(id);
//        [HttpPut("{id}")]
//        public Task<IActionResult> Update(Guid id, [FromBody] Account entity) => _controller.Update(id, entity);
//        [HttpGet("{id}/history")]
//        public Task<IActionResult> GetHistory(Guid id) => _historicalCrudCrudController.GetHistory(id);
//        [HttpPost("{id}/restore")]
//        public Task<IActionResult> Restore(Guid id) => _historicalCrudCrudController.Restore(id);

//        public Task<IActionResult> HeadById(Guid id)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
