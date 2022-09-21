using CoMute.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CoMute.Web.Data.Repository
{
    public class CarPoolRepository
    {
        private readonly CoMuteDbContext _coMuteDbContext;
        public CarPoolRepository( )
        {
            _coMuteDbContext = new CoMuteDbContext(); 
        }

        public async Task AddCarPool(CarPool model)
        {
            _coMuteDbContext.CarPools.Add(model);
            await _coMuteDbContext.SaveChangesAsync();
        }
        
        public CarPool GetCarPoolById(int Id)
        {
            return _coMuteDbContext.CarPools.Find(Id);
        }
        
        public List<CarPool> GetCarPoolsByUserName(string userName)
        {
            return _coMuteDbContext.CarPools.Where(x=>x.Owner_Leader==userName).ToList();
        }
        
        public List<CarPool> GetAllCarPools()
        {
            return _coMuteDbContext.CarPools.ToList();
        }

        public async Task JoinCarPool(int carPoolId, string userId)
        {
            _coMuteDbContext.UserCarPool.Add(new UserCarPool { CarPoolId=carPoolId, UserId=userId, DateJoined = DateTime.Now});
            await _coMuteDbContext.SaveChangesAsync();
        }

        public List<UserCarPool> GetCarPoolsUserJoined(string userId)
        {
            return _coMuteDbContext.UserCarPool.Where(x => x.UserId == userId).Include(x => x.CarPool).ToList();
        }

    }
}