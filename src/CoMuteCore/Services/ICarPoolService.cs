using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoMuteCore.Models;

namespace CoMuteCore.Services
{
    public interface ICarPoolService
    {
        public Task CreateCarPoolAsync(CarPool carPool);
        public Task<IReadOnlyList<CarPool>> GetAllCarPoolsAsync();
        public Task<CarPool> ViewCarPoolAsync(int id);
    }
}