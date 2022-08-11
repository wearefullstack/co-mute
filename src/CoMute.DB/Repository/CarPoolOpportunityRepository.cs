using CoMute.Core.Domain;
using CoMute.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.DB.Repository
{
    public class CarPoolOpportunityRepository : ICarPoolOpportunityRepository
    {
        readonly ICoMuteDbContext _coMuteDbContext;
        public CarPoolOpportunityRepository(ICoMuteDbContext coMuteDbContext)
        {
            _coMuteDbContext = coMuteDbContext ?? throw new ArgumentNullException(nameof(coMuteDbContext));
        }
        public void DeleteCarPoolOpportunity(CarPoolOpportunity carPoolOpportunity)
        {
            throw new NotImplementedException();
        }

        public List<CarPoolOpportunity> GetAllCarPools()
        {
            return _coMuteDbContext.CarPoolOpportunities.ToList();
        }

        public CarPoolOpportunity GetById(Guid? carPoolOpportunityId)
        {
            throw new NotImplementedException();
        }

        public void Save(CarPoolOpportunity carPoolOpportunity)
        {
            if (carPoolOpportunity == null) throw new ArgumentNullException(nameof(carPoolOpportunity));

            _coMuteDbContext.CarPoolOpportunities.Add(carPoolOpportunity);
            _coMuteDbContext.SaveChanges();
        }

        public void Update(CarPoolOpportunity carPoolOpportunity)
        {
            throw new NotImplementedException();
        }
    }
}
