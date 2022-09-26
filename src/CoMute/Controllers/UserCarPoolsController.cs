using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoMute.Interfaces;
using CoMute.Models;
using CoMute.Models.Requests;

namespace CoMute.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UserCarPoolsController : ControllerBase
  {
    private readonly IUserCarPoolService _userCarPoolService;

    public UserCarPoolsController(IUserCarPoolService userCarPoolService)
    {
      _userCarPoolService = userCarPoolService;
    }

    [HttpGet]
    [Route("{userId}")]
    public IActionResult GetUserJoinedCarPools(int userId)
    {
      try
      {
        List<UserCarPool> userJoinedCarPools = _userCarPoolService.GetUserCarPools(userId);

        if (userJoinedCarPools != null)
        {
          return Ok(userJoinedCarPools);
        }
        else
        {
          return BadRequest("Something went wrong when getting user joined car pools");
        }
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("join")]
    public async Task<IActionResult> JoinCarPool([FromBody] UserCarPoolRequest model)
    {
      try
      {
        var response = await _userCarPoolService.JoinCarPoolAsync(model.CarPoolId, model.UserId);

        if (response > 0)
        {
          List<UserCarPool> userJoinedCarPools = _userCarPoolService.GetUserCarPools(model.UserId);
          return Ok(userJoinedCarPools);
        }
        else
        {
          return BadRequest("Something went wrong, error when joining car pool");
        }
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("leave")]
    public async Task<IActionResult> LeaveCarPool([FromBody] UserCarPoolRequest model)
    {
      try
      {
        var response = await _userCarPoolService.LeaveCarPoolAsync(model.CarPoolId, model.UserId);

        if (response > 0)
        {
          List<UserCarPool> userJoinedCarPools = _userCarPoolService.GetUserCarPools(model.UserId);
          return Ok(userJoinedCarPools);
        }
        else
        {
          return BadRequest("Something went wrong, error when leaving car pool");
        }
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }
  }
}
