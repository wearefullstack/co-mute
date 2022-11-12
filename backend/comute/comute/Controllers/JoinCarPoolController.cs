using Microsoft.AspNetCore.Mvc;

namespace comute.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JoinCarPoolController : Controller
{
    [HttpGet("all")]
    public IActionResult GetJoinedCarPools()
    {
        return Ok();
    }
 
    [HttpGet("myCarPools/{userId:int}")]
    public IActionResult MyJoinedCarPools(int userId)
    {
        return Ok(userId);
    }

    [HttpPost("carPool/{carPoolId:int}/user/{userId:int}")]
    public IActionResult SaveJoinCarPool(int carPoolId, int userId)
    {
        return Ok(new { CarPool = carPoolId, User = userId });
    }

    [HttpPost("leave/{joinId:int}")]
    public IActionResult LeaveCarPoolOpportunity(int joinId)
    {
        return Ok(joinId);
    }
}
