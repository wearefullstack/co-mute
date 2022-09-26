using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using CoMute.Interfaces;
using CoMute.Models;
using Microsoft.Extensions.Logging;
using CoMute.Helpers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CoMute.Services
{
  public class UserCarPoolService : IUserCarPoolService
  {
    private readonly MainDbContext _database;
    private readonly ILogger<UserCarPoolService> _logger;

    public UserCarPoolService(MainDbContext database, ILogger<UserCarPoolService> logger)
    {
      _logger = logger;
      _database = database;
    }

    public List<UserCarPool> GetUserCarPools(int userId)
    {
      //return list of carpools that this user has already joined
      try
      {
        return _database.UserCarPools.Where(ucp => ucp.UserId == userId).ToList();
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message);
        throw;
      }
    }

    public async Task<int> JoinCarPoolAsync(int carPoolId, int userId)
    {
      try
      {
        UserCarPool userCarPool = new UserCarPool(userId, carPoolId);
        //check if user is already in the car pool
        var checkRecord = _database.UserCarPools.Where(ucp => ucp.UserId == userId &&
          ucp.CarPoolId == carPoolId).FirstOrDefault(); 
        if(checkRecord != null)
        {
          throw new Exception("User is already in the car pool");
        }

        //check if car pool is at its limit
        var carPool = _database.CarPools.Find(carPoolId);
        var carPoolPassengers = _database.UserCarPools.Where(ucp => ucp.CarPoolId == carPoolId).ToList();
        if(carPool.AvailableSeats <= carPoolPassengers.Count())
        {
          throw new Exception("Maximum number of passangers has reached");
        }

        /*TODO: check if user time lapse with other joined or created car pools, 
         * by days available, departure and arrival time*/

        await _database.UserCarPools.AddAsync(userCarPool);
        int response = await _database.SaveChangesAsync();
        return response;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message);
        throw ex;
      }

    }

    public async Task<int> LeaveCarPoolAsync(int carPoolId, int userId)
    {
      try
      {
        UserCarPool userCarPool = _database.UserCarPools.Where(ucp => ucp.UserId == userId &&
          ucp.CarPoolId == carPoolId).FirstOrDefault();
        if(userCarPool != null)
        {
          _database.UserCarPools.Remove(userCarPool);
        }
        var response = await _database.SaveChangesAsync();
        return response;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message);
        throw ex;
      }
    }
  }
}
