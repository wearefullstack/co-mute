using CoMute.Core.Domain;
using CoMute.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

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
            if (carPoolOpportunity == null) throw new ArgumentNullException(nameof(carPoolOpportunity));

            _coMuteDbContext.CarPoolOpportunities.Remove(carPoolOpportunity);
            _coMuteDbContext.SaveChanges();
        }

        public List<CarPoolOpportunity> GetAllCarPools()
        {
            return _coMuteDbContext.CarPoolOpportunities.ToList();
        }

        public CarPoolOpportunity GetById(Guid? carPoolOpportunityId)
        {
            if (carPoolOpportunityId == Guid.Empty) throw new ArgumentNullException(nameof(carPoolOpportunityId));
            return _coMuteDbContext.CarPoolOpportunities.FirstOrDefault(x => x.CarPoolId == carPoolOpportunityId);
        }

        public void Save(CarPoolOpportunity carPoolOpportunity)
        {
            if (carPoolOpportunity == null) throw new ArgumentNullException(nameof(carPoolOpportunity));

            _coMuteDbContext.CarPoolOpportunities.Add(carPoolOpportunity);
            _coMuteDbContext.SaveChanges();
        }

        public void Update(CarPoolOpportunity carPoolOpportunity)
        {
            if (carPoolOpportunity == null) throw new ArgumentNullException(nameof(carPoolOpportunity));
            _coMuteDbContext.CarPoolOpportunities.AddOrUpdate(carPoolOpportunity);
            _coMuteDbContext.SaveChanges();
        }
    }
}
