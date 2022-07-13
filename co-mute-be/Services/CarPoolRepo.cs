using co_mute_be.Abstractions.Interfaces;
using co_mute_be.Database;
using co_mute_be.Models;

namespace co_mute_be.Services
{
    public class CarPoolRepo : IDataRepository<CarPoolRepo>
    {

        private readonly CarPoolOppContext _context;

        public CarPoolRepo(CarPoolOppContext context)
        {
            _context = context;
        }

        public async Task<List<CarPoolRepo>> GetManyAsync(int skip, int limit)
        {
            throw new NotImplementedException();
        }

        public async void CreateAsync(CarPoolOpp item)
        {
            if (_context.CarPoolOpps == null)
            {
                throw new NullReferenceException("Entity set 'CarPoolOppContext.CarPoolOpps' is null.");
            }
            _context.CarPoolOpps.Add(item);
            await _context.SaveChangesAsync();

        }

        public CarPoolRepo Delete(long id)
        {
            throw new NotImplementedException();
        }

        public CarPoolRepo GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public CarPoolRepo Update(CarPoolRepo update)
        {
            throw new NotImplementedException();
        }

        Task<CarPoolRepo> IDataRepository<CarPoolRepo>.GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<CarPoolRepo> Create(CarPoolRepo item)
        {
            throw new NotImplementedException();
        }

        Task<CarPoolRepo> IDataRepository<CarPoolRepo>.Delete(long id)
        {
            throw new NotImplementedException();
        }

        Task<CarPoolRepo> IDataRepository<CarPoolRepo>.Update(CarPoolRepo update)
        {
            throw new NotImplementedException();
        }
    }
}
