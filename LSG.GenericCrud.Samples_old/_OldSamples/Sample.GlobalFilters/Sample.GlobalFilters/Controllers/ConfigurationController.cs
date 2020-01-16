using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sample.GlobalFilters.Controllers
{
    [Route("api/[controller]")]
    public class ConfigurationController : Controller
    {
        [Route("{key}")]
        public IActionResult Get(string key)
        {
            return Ok(HttpContext.Session.GetString(key));
        }

        [Route("{key}")]
        [HttpPost]
        public IActionResult Set(string key, [FromBody] string value)
        {
            HttpContext.Session.SetString(key, value);
            return Ok();
        }
    }
}
