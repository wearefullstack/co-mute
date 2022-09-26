using CoMute.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoMute.Models;
using System.Data;
using Microsoft.Extensions.Logging;
using CoMute.Helpers;
using CoMute.Enums;

namespace CoMute.Services
{
  public class CarPoolService : ICarPoolService
  {
    private readonly MainDbContext _database;
    private readonly ILogger<CarPoolService> _logger;

    public CarPoolService(MainDbContext database, ILogger<CarPoolService> logger)
    {
      _logger = logger;
      _database = database;
    }

    public async Task<int> AddCarPoolAsync(CarPool carPool)
    {
      try
      {
        int response = 0;
        carPool.Created = DateTime.Now;
        carPool.Modified = DateTime.Now;
        await _database.CarPools.AddAsync(carPool);
        response = await _database.SaveChangesAsync();
        return response;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message);
        throw ex;
      }
    }

    public CarPool GetCarPool(int carPoolId)
    {
      return _database.CarPools.Where(c => c.Id == carPoolId).FirstOrDefault();
    }

    public async Task<int> UpdateCarPoolAsync(CarPool carPool)
    {
      try
      {
        int response = 0;
        CarPool _carPool = _database.CarPools.Where(c => c.Id == carPool.Id).FirstOrDefault();
        if (_carPool != null)
        {
          UpdatePropertyChange(_carPool, carPool, "DepartureTime");
          UpdatePropertyChange(_carPool, carPool, "ArrivalTime");
          UpdatePropertyChange(_carPool, carPool, "Origin");
          UpdatePropertyChange(_carPool, carPool, "AvailableSeats");
          UpdatePropertyChange(_carPool, carPool, "DaysAvailable");
          UpdatePropertyChange(_carPool, carPool, "Destination");
          UpdatePropertyChange(_carPool, carPool, "Notes");

          _carPool.Modified = DateTime.Now;
          _database.Entry(_carPool).Property(cp => cp.Modified).IsModified = true;
          response = await _database.SaveChangesAsync();
        }
        return response;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message);
        throw ex;
      }
    }

    private void UpdatePropertyChange(CarPool oldCP, CarPool newCP, string property)
    {
      //update only changed properties
      if (!ExtentionMethods.ComparePropertyValues(oldCP, newCP, property))
      {
        var typeName = oldCP.GetType().GetProperty(property).PropertyType.Name;
        if (typeName == typeof(bool).Name)
        {
          oldCP.GetType().GetProperty(property).SetValue(oldCP,
            ExtentionMethods.GetPropertyValue<bool>(newCP, property), null);
        }
        else if (typeName == typeof(int).Name)
        {
          oldCP.GetType().GetProperty(property).SetValue(oldCP,
            ExtentionMethods.GetPropertyValue<int>(newCP, property), null);
        }
        else
        {
          oldCP.GetType().GetProperty(property).SetValue(oldCP,
            ExtentionMethods.GetPropertyValue<string>(newCP, property), null);
        }
      }
    }

    public List<CarPool> GetCarPools(int userId, CarPoolFilters carPoolFilter)
    {
      switch (carPoolFilter)
      {
        case CarPoolFilters.Joined:
          List<int> joinedUserCarPoolIds = _database.UserCarPools.Where(ucp => ucp.UserId == userId)
            .Select(ucp => ucp.CarPoolId).ToList();
          return _database.CarPools.Where(cp => joinedUserCarPoolIds.Contains(cp.Id)).ToList();
        case CarPoolFilters.Available:
          List<int> availableUserCarPoolIds = _database.UserCarPools.Where(ucp => ucp.UserId == userId)
            .Select(ucp => ucp.CarPoolId).ToList();
          return _database.CarPools.Where(cp => !availableUserCarPoolIds.Contains(cp.Id))
            .Where(cp => cp.OwnerId != userId).ToList();
        case CarPoolFilters.Mine:
          return _database.CarPools.Where(cp => cp.OwnerId == userId).ToList();
        default:
          return _database.CarPools.ToList();
      }
    }
  }
}
