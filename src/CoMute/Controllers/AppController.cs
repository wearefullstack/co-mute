using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoMute.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppController : ControllerBase
    {
        public AppController(){}

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Co-Mute API running");
        }
    }
}
