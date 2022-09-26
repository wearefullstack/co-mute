using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoMute.Interfaces;
using CoMute.Models;
using CoMute.Enums;

namespace CoMute.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CarPoolsController : ControllerBase
  {
    private readonly ICarPoolService _carPoolService;

    public CarPoolsController(ICarPoolService carPoolService)
    {
      _carPoolService = carPoolService;
    }

    [HttpGet]
    [Route("{carPoolId}")]
    public IActionResult GetCarPool(int carPoolId)
    {

      try
      {
        var user = _carPoolService.GetCarPool(carPoolId);

        if (user != null)
        {
          return Ok(user);
        }
        else
        {
          return BadRequest("CarPool not found");
        }
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet]
    [Route("{userId}/{carPoolFilter}")]
    public IActionResult GetCarPools(int userId,CarPoolFilters carPoolFilter = CarPoolFilters.All)
    {
      try
      {
        var carPools = _carPoolService.GetCarPools(userId, carPoolFilter);

        if (carPools != null)
        {
          return Ok(carPools);
        }
        else
        {
          return BadRequest("CarPool not found");
        }
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddCarPool([FromBody] CarPool model)
    {

      try
      {
        //TODO: check if I have any car pools with the conflicting times

        var carPool = await _carPoolService.AddCarPoolAsync(model);

        if (carPool > 0)
        {
          return Ok(new ContentResult { StatusCode = (int)HttpStatusCode.OK, ContentType = "application/json", Content = "carPool was added successfully" });
        }
        else
        {
          return BadRequest("Something went wrong, error adding carPool");
        }
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateCarPool([FromBody] CarPool model)
    {

      try
      {
        var carPool = await _carPoolService.UpdateCarPoolAsync(model);

        if (carPool > 0)
        {
          return Ok(new ContentResult { StatusCode = (int)HttpStatusCode.OK, ContentType = "application/json", Content = "carPool was updated successfully" });
        }
        else
        {
          return BadRequest("Something went wrong, error updating car pool");
        }
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }
  }
}
