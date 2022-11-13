using comute.client.CarPool;
using comute.Models;
using comute.Services.CarPoolService;
using comute.Services.JoinService;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace comute.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CarPoolController : Controller
{
    private readonly ICarPoolService _carPoolService;
    private readonly IJoinService _joinService;

    public CarPoolController(ICarPoolService carPoolService, IJoinService joinService) {
        _carPoolService = carPoolService;
        _joinService = joinService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetCarPools()
    {
        List<CarPoolInfo> list = await _carPoolService.GetCarPools();
        return Ok(list);
    }

    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> GetCarPoolCurrentUser(int userId)
    {
        List<CarPoolInfo> list = await _carPoolService.GetCarPoolCurrentUser(userId);
        return Ok(list);
    }

    [HttpPost("create/{userId:int}")]
    public async Task<IActionResult> SaveCarPool(int userId, CarPoolRequest request)
    {
        try
        {
            var carPool = AddCarPool(userId, request);
            var myExistingCarPools = await _carPoolService.GetCarPoolCurrentUser(userId);
            var isOverlapping = false;

            foreach (var item in myExistingCarPools)
            {
                if (item.ExpectedArrivalTime >= request.DepartureTime
                    && item.DepartureTime <= request.ExpectedArrivalTime)
                {
                    isOverlapping = true;
                    break;
                }
                if(item.DepartureTime == request.DepartureTime
                    && item.ExpectedArrivalTime == request.ExpectedArrivalTime)
                {
                    isOverlapping = true;
                    break;
                }
            }
            if (!isOverlapping)
                await _carPoolService.SaveCarPool(carPool);

            var response = !isOverlapping ? CarPoolResponseBack(carPool) : CarPoolResponseBack(new CarPool());
            if (response.CarPoolId > 0)
            {
                await _joinService.SaveJoinCarPool(response.CarPoolId, userId, joinCarPool: new JoinCarPool
                {
                    JoinId = 0,
                    UserId = userId,
                    CarPoolId = response.CarPoolId,
                    JoinedOn = DateTime.Now
                });
            }
            return CreatedAtAction(
                        actionName: nameof(GetCarPoolCurrentUser),
                        routeValues: new { userId = carPool.Owner },
                        value: response
                    );
        }
        catch
        {
            return Problem();
        }
    }

    [NonAction]
    private static CarPool AddCarPool(int userId, CarPoolRequest request)
    {
        return new CarPool
        {
            CarPoolId = 0,
            Origin = request.Origin,
            Destination = request.Destination,
            DepartureTime = request.DepartureTime,
            ExpectedArrivalTime = request.ExpectedArrivalTime,
            DaysAvailable = string.Join(",", request.DaysAvailable),
            AvailableSeats = request.AvailableSeats,
            Owner = userId,
            Notes = request.Notes,
            Active = true
        };
    }

    [NonAction]
    private static CarPoolResponse CarPoolResponseBack(CarPool carPool)
    {
        try
        {
            return new CarPoolResponse
            {
                CarPoolId = carPool.CarPoolId,
                Origin = carPool.Origin,
                Destination = carPool.Destination,
                DepartureTime = carPool.DepartureTime,
                ExpectedArrivalTime = carPool.ExpectedArrivalTime,
                DaysAvailable = carPool.DaysAvailable.Split(",").ToList(),
                AvailableSeats = carPool.AvailableSeats,
                Owner = carPool.Owner,
                Notes = carPool.Notes,
                Active = carPool.Active
            };
        }
        catch
        {
            return new CarPoolResponse();
        }
    }
}
