using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Sample.CrudOptions.Controllers
{
    [Route("api/[controller]")]
    public class TokensController : Controller
    {
        [Route("accessToken")]
        [AllowAnonymous]
        public IActionResult GetAccessToken()
        {
            var client = new RestClient("https://lonesomegeek.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"client_id\":\"vAnevpML1exaQxv6wwzrFaAJhHHUPBB8\",\"client_secret\":\"7ibb2fALHH5FwOhciL0u_r8cgLowRB8-e8L38VQRCEX9QFtPsubjwPocbd1v3okJ\",\"audience\":\"https://genericcrud.lonesomegeek.com\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return Ok(response.Content);
        }
    }
}
