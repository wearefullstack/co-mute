using Microsoft.AspNetCore.Mvc;

namespace comute.Controllers;

public class ErrorController : Controller
{
    [NonAction]
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}
