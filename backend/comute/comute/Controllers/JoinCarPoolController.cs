using comute.Models;
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
    public async Task<IActionResult> SaveJoinCarPool(int carPoolId, int userId)
    {
        try
        {
            var joinCarPool = SaveCarPoolRequest(carPoolId, userId);
            var existingJoinedPools = await _joinService.JoinedCarPools(userId);
            bool isOverlapping = false;
            bool isDuplicate = false;

            if (existingJoinedPools != null || existingJoinedPools.Count > 0)
                isOverlapping = checkIfOverlapping(existingJoinedPools, joinCarPool);

            if (!isOverlapping)
                isDuplicate = checkAlreadyExist(carPoolId, existingJoinedPools);

            if (!isDuplicate && !isOverlapping)
                isDuplicate = await _joinService.SaveJoinCarPool(carPoolId, userId, joinCarPool);

            var response = !isDuplicate && !isOverlapping ?
                JoinInfoResponse(joinCarPool) : JoinInfoResponse(new JoinCarPool());

            return CreatedAtAction(
                        actionName: nameof(MyJoinedCarPools),
                        routeValues: new { userId = joinCarPool.UserId },
                        value: response
                    );
        }
        catch
        {
            return Problem();
        }
    }

    [HttpPost("leave/{joinId:int}")]
    public async Task<IActionResult> LeaveCarPoolOpportunity(int joinId)
    {
        await _joinService.LeaveCarPoolOpportunity(joinId);
        return NoContent();
    }

    [NonAction]
    private static JoinCarPool SaveCarPoolRequest(int carPoolId, int userId)
    {
        return new JoinCarPool
        {
            JoinId = 0,
            UserId = userId,
            CarPoolId = carPoolId,
            JoinedOn = DateTime.Now
        };
    }

    [NonAction]
    private static JoinCarPool JoinInfoResponse(JoinCarPool joinCarPool)
    {
        try
        {
            return new JoinCarPool
            {
                JoinId = joinCarPool.JoinId,
                UserId = joinCarPool.UserId,
                CarPoolId = joinCarPool.CarPoolId,
                JoinedOn = joinCarPool.JoinedOn
            };
        }
        catch
        {
            return new JoinCarPool();
        }
    }

    [NonAction]
    private static bool checkAlreadyExist(int carPoolId, List<JoinInfo> list)
    {
        bool result = false;
        foreach (var item in list)
        {
            if (item.CarPoolId == carPoolId)
            {
                result = true;
                break;
            }
        }
        return result;
    }

    [NonAction]
    private static bool checkIfOverlapping(List<JoinInfo> list, JoinCarPool joinCarPool)
    {
        bool result = false;
        foreach (var item in list)
        {
            if (item.JoinedOn >= joinCarPool.JoinedOn)
            {
                result = true;
                break; 
            }
        }
        return result;
    }
}
