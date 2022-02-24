using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LookupTableAdminDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        // GET: api/<AboutController>
        [HttpGet]
        public string Get()
        {
            return "About Controller";
        }
    }
}
