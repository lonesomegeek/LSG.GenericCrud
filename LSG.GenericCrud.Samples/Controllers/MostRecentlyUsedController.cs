using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.App.Controllers
{
    public class MostRecentlyUsedController : ControllerBase
    {
        private readonly ICrudRepository _repository;
        private readonly IUserInfoRepository _userInfoReposiory;

        public MostRecentlyUsedController(
            ICrudRepository repository,
            IUserInfoRepository userInfoReposiory)
        {
            _repository = repository;
            _userInfoReposiory = userInfoReposiory;
        }

        [HttpGet("most-recently-used")]
        public async Task<IActionResult> GetMostRecenlyUsed()
        {

            var events = _repository
                .GetAllAsync<Guid, HistoricalEvent>().Result
                .Where(_ =>
                    _.Action == HistoricalActions.Read.ToString() &&
                    _.CreatedBy == _userInfoReposiory.GetUserInfo())
                .OrderByDescending(_ => _.CreatedDate)
                .GroupBy(_ => _.EntityId)
                .Select(_ => new
                {                    
                    EntityId = _.Key,                    
                    LastEvent = _.OrderByDescending(e => e.CreatedDate).FirstOrDefault()

                });
                //.Take(10);

            return Ok(events);
        }
    }
}
