﻿using comute.client.CarPool;
using comute.Models;
using comute.Services.CarPoolService;
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

    public CarPoolController(ICarPoolService carPoolService) =>
        _carPoolService = carPoolService;

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
        var carPool = AddCarPool(userId, request);
        var myExistingCarPools = await _carPoolService.GetCarPoolCurrentUser(userId);
        var isOverlapping = false;

        if(myExistingCarPools != null || myExistingCarPools.Count > 0)
        {
            foreach (var item in myExistingCarPools)
            {
                if (item.ExpectedArrivalTime >= request.DepartureTime
                    && item.DepartureTime <= request.ExpectedArrivalTime)
                {
                    isOverlapping = true;
                    break;
                }
            }
        }
        if (!isOverlapping)
           await _carPoolService.SaveCarPool(carPool);

        var response = !isOverlapping ? CarPoolResponseBack(carPool) : CarPoolResponseBack(new CarPool());

        return CreatedAtAction(
                    actionName: nameof(GetCarPoolCurrentUser),
                    routeValues: new { userId = carPool.Owner },
                    value: response
                );
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
#pragma warning disable CS8601 // Possible null reference assignment.
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
#pragma warning restore CS8601 // Possible null reference assignment.
    }
}
