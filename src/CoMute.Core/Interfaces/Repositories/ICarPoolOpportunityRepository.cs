using CoMute.Core.Domain;
using System;
using System.Collections.Generic;

namespace CoMute.Core.Interfaces.Repositories
{
    public interface ICarPoolOpportunityRepository
    {
        List<CarPoolOpportunity> GetAllCarPools();
        void Save(CarPoolOpportunity carPoolOpportunity);
        CarPoolOpportunity GetById(Guid? carPoolOpportunityId);
        void DeleteCarPoolOpportunity(CarPoolOpportunity carPoolOpportunity);
        void Update(CarPoolOpportunity carPoolOpportunity);
    }
}
