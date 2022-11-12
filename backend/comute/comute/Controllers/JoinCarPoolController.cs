using comute.Services.JoinService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace comute.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class JoinCarPoolController : Controller
{
    private readonly IJoinService _joinService;

    public JoinCarPoolController(IJoinService joinService) => _joinService = joinService;

    [HttpGet("all")]
    public async Task<IActionResult> GetJoinedCarPools()
    {
        var list = await _joinService.AllCarPoolsJoined();
        return Ok(list);
    }
 
    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> MyJoinedCarPools(int userId)
    {
        var list = await _joinService.JoinedCarPools(userId);
        return Ok(list);
    }

    [HttpPost("carPool/{carPoolId:int}/user/{userId:int}")]
    public IActionResult SaveJoinCarPool(int carPoolId, int userId)
    {
        return Ok(new { CarPool = carPoolId, User = userId });
    }

    [HttpPost("leave/{joinId:int}")]
    public async Task<IActionResult> LeaveCarPoolOpportunity(int joinId)
    {
        await _joinService.LeaveCarPoolOpportunity(joinId);
        return NoContent();
    }
}
