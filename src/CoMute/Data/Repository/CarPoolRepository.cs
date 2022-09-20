using CoMute.Web.Data.Entities;
using System;
using System.Collections.Generic;
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
        
        public async Task JoinCarPool(int carPoolId, string userId)
        {
            _coMuteDbContext.UserCarPool.Add(new UserCarPool { CarPoolId=carPoolId, UserId=userId});
            await _coMuteDbContext.SaveChangesAsync();
        }

    }
}