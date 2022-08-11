using CoMute.Core.Domain;
using CoMute.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoMute.DB.Repository
{
    public class JoinedCarPoolsOpportunityRepository : IJoinedCarPoolsOpportunityRepository
    {
        readonly ICoMuteDbContext _coMuteDbContext;
        public JoinedCarPoolsOpportunityRepository(ICoMuteDbContext coMuteDbContext)
        {
            _coMuteDbContext = coMuteDbContext ?? throw new ArgumentNullException(nameof(coMuteDbContext));
        }

        public void DeleteJoinedCarPoolOpportunity(JoinCarPoolsOpportunity joinedCarPoolOpportunity)
        {
            if (joinedCarPoolOpportunity == null) throw new ArgumentNullException(nameof(joinedCarPoolOpportunity));
            _coMuteDbContext.JoinCarPoolsOpportunities.Remove(joinedCarPoolOpportunity);
            _coMuteDbContext.SaveChanges();
        }

        public List<JoinCarPoolsOpportunity> GetAllJoinedCarPools()
        {
            return _coMuteDbContext.JoinCarPoolsOpportunities.ToList();
        }

        public List<JoinCarPoolsOpportunity> GetAllUserJoinedCarPools(Guid userId)
        {
            return _coMuteDbContext.JoinCarPoolsOpportunities.Where(x => x.UserId == userId).ToList();
        }

        public JoinCarPoolsOpportunity GetBy(Guid? userId, Guid carPoolId)
        {
            return _coMuteDbContext
               .JoinCarPoolsOpportunities
               .FirstOrDefault(x => x.UserId == userId && x.CarPoolId == carPoolId);
        }

        public JoinCarPoolsOpportunity GetById(Guid? joinedCarPoolOpportunityId)
        {
            return _coMuteDbContext
                .JoinCarPoolsOpportunities
                .FirstOrDefault(x => x.JoinCarPoolsOpportunityId == joinedCarPoolOpportunityId);
        }

        public void Save(JoinCarPoolsOpportunity joinCarPoolsOpportunity)
        {
            if (joinCarPoolsOpportunity == null) throw new ArgumentNullException(nameof(joinCarPoolsOpportunity));

            _coMuteDbContext.JoinCarPoolsOpportunities.Add(joinCarPoolsOpportunity);
            _coMuteDbContext.SaveChanges();
        }
    }
}
