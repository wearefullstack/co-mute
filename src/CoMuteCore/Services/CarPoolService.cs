using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoMuteCore.Data;
using CoMuteCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CoMuteCore.Services
{
    public class CarPoolService : ICarPoolService
    {
        private readonly ApplicationContext _ctx;
        public CarPoolService(ApplicationContext ctx)
        {
            _ctx = ctx;
        }

        public async Task CreateCarPoolAsync(CarPool carPool)
        {
            await _ctx.CarPools.AddAsync(carPool);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<CarPool>> GetAllCarPoolsAsync()
        {
            return await _ctx.CarPools.ToListAsync();
        }

        public async Task<CarPool> ViewCarPoolAsync(int id)
        {
            return await _ctx.CarPools.FirstOrDefaultAsync(x => x.ID == id);
        }
    }
}