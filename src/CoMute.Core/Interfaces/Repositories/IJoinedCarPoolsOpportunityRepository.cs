using CoMute.Core.Domain;
using System;
using System.Collections.Generic;

namespace CoMute.Core.Interfaces.Repositories
{
    public interface IJoinedCarPoolsOpportunityRepository
    {
        List<JoinCarPoolsOpportunity> GetAllJoinedCarPools();
        List<JoinCarPoolsOpportunity> GetAllUserJoinedCarPools(Guid userId);
        void Save(JoinCarPoolsOpportunity joinCarPoolsOpportunity);
        JoinCarPoolsOpportunity GetById(Guid? joinedCarPoolOpportunityId);
        void DeleteJoinedCarPoolOpportunity(JoinCarPoolsOpportunity joinedCarPoolOpportunity);        
        JoinCarPoolsOpportunity GetBy(Guid? userId, Guid carPoolId);
    }
}
