using Microsoft.AspNetCore.Mvc;

namespace Hiper.Dev.TestesUnitarios.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        [HttpGet(Name = "Get")]
        public async Task<ActionResult> Get()
        {
            return Ok();
        }
    }
}