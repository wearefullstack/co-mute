using CoMute.API.Helpers;
using CoMute.API.Models.Opportunity;
using CoMute.Data.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CoMute.API.Services.Opportunity
{
    public class OpportunityService : IOpportunityService
    {
        private readonly OpportunityDbContext _dbContext;
        private readonly IConfiguration configuration;
        public OpportunityService(OpportunityDbContext dbContext,IConfiguration configuration)
        {
            _dbContext = dbContext;
            this.configuration = configuration;
        }

        private string GetConnectionString()
        {
            return configuration["ConnectionStrings:DefaultConnection"].ToString();
        }
        /// <summary>
        /// Gets all available Opportunities created
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SearchOpportunityModel>> GetOpportunityAsync()
        {
            var opportunities = await (from up in _dbContext.UserOpportunity
                                       join o in _dbContext.CarPoolOpportunity on up.OpportunityId equals o.OpportunityId
                                       join u in _dbContext.Users on up.UserId equals u.Id
                                       select new SearchOpportunityModel 
                                       {
                                          OpportunityId  = up.OpportunityId ,
                                          UserId = up.UserId,
                                          UserName = u.Name + " " + u.Surname,
                                          OpportunityName = o.OpportunityName, 
                                          DepartureTime = o.DepartureTime.ToString(),
                                          ArrivalTime = o.ArrivalTime.ToString(),
                                          Origin = o.Origin,
                                          DaysAvailable = o.DaysAvailable, 
                                          Destination  = o.Destination,
                                          AvailableSeats = o.AvailableSeats,
                                          Notes = o.Notes,
                                          IsLeader = up.IsLeader ? "Leader": "Joined",
                                          CreatedDate = o.CreatedDate.ToString(),
                                          JoinDate = up.JoinDate.ToString()
                                        }).ToListAsync();
            return opportunities;
        }

        /// <summary>
        /// Gets a list of opportunities for a logged in user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SearchOpportunityModel>> GetOpportunityByUserAsync(string userId)
        {
            var opportunities = await (from up in _dbContext.UserOpportunity
                                       join o in _dbContext.CarPoolOpportunity on up.OpportunityId equals o.OpportunityId
                                       join u in _dbContext.Users on up.UserId equals u.Id
                                       where up.UserId == userId
                                       select new SearchOpportunityModel
                                       {
                                           OpportunityId = up.OpportunityId,
                                           UserId = up.UserId,
                                           UserName = u.Name + " " + u.Surname,
                                           OpportunityName = o.OpportunityName,
                                           DepartureTime = o.DepartureTime.ToString(),
                                           ArrivalTime = o.ArrivalTime.ToString(),
                                           Origin = o.Origin,
                                           DaysAvailable = o.DaysAvailable,
                                           Destination = o.Destination,
                                           AvailableSeats = o.AvailableSeats,
                                           Notes = o.Notes,
                                           IsLeader = up.IsLeader ? "Leader" : "Joined",
                                           CreatedDate = o.CreatedDate.ToString(),
                                           JoinDate = up.JoinDate.ToString()
                                       }).ToListAsync();
            return opportunities;
        }

        public async Task<string> JoinOpportunityAsync(JoinOpportunityModel model)
        {
            string returnValue = "FAILED.Unable to Join Opportunity";
            //then addds to userOpportunity table to insert user record as a Lead on the Opportunity

            //overlap time frame check
            var overlappingOpportunity = await _dbContext.CarPoolOpportunity.ToListAsync();
            bool timeResult = true,capacityResult = true;

            foreach (var o in overlappingOpportunity)
            {
                timeResult = TimeFrameHelper.CheckTimeFrames(o.DepartureTime, o.ArrivalTime, model.DepartureTime, model.ArrivalTime);

                //if result is false, meaning there is time frame that overlaps exit the loop
                if (!timeResult)
                    break;
            }

            //check capacity
            var getOpportunity = await _dbContext.CarPoolOpportunity.Where(x=>x.OpportunityId == model.OpportunityId).FirstOrDefaultAsync();
            if (getOpportunity.AvailableSeats <= 0)
                capacityResult = false;

            if (!timeResult)
                returnValue = $"FAILED.Cannot Join an opportunity with overlapping time frames!";
            else if(!capacityResult)
                returnValue = $"FAILED.Cannot Join an opportunity with {getOpportunity.AvailableSeats} seats available.";
            else if(!timeResult && !capacityResult)
                returnValue = $"FAILED.Cannot Join an opportunity with {getOpportunity.AvailableSeats} seats available. and overlapping time frames!";
            else
            {
                var userOpportunity = new UserOpportunity()
                {
                    UserId = model.UserId,
                    OpportunityId = model.OpportunityId,
                    IsLeader = false,//when a lead user creates opportunity ,this is set to true
                    JoinDate = DateTime.Now
                };
                await _dbContext.UserOpportunity.AddAsync(userOpportunity);
                await _dbContext.SaveChangesAsync();

                var createdUserOpportunityId = userOpportunity.Id;
                if (createdUserOpportunityId > 0)
                {
                    //will subtract a seat everytime a member joins
                    getOpportunity.AvailableSeats = getOpportunity.AvailableSeats - 1;

                    _dbContext.CarPoolOpportunity.Update(getOpportunity);
                    await _dbContext.SaveChangesAsync();
                    returnValue = "SUCCESS.Joined Opportunity successfully";
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Gets the current user linked to an opportunity, if it finds the record it will go ahead and delete or leave the opportunity linked to the user
        /// </summary>
        /// <param name="leaveOpportunity"></param>
        /// <returns></returns>
        public async Task<string> LeaveOpportunityAsync(LeaveOpportunityModel leaveOpportunity)
        {
            string returnValue = "FAILED.Could not Leave Opportunity.Error occurred during the process";
            var opportunity = await _dbContext.UserOpportunity.Where(x => x.OpportunityId == leaveOpportunity.OpportunityId && x.UserId == leaveOpportunity.UserId).ToListAsync();
            if (opportunity.Count() > 0)
            {
                var op = await _dbContext.UserOpportunity.Where(x => x.OpportunityId == leaveOpportunity.OpportunityId && x.UserId == leaveOpportunity.UserId).FirstOrDefaultAsync();

                 _dbContext.UserOpportunity.Remove(op);
                await _dbContext.SaveChangesAsync();

                //adds a seat back to opportunity if a user left that opportunity
                var getOpportunity = await _dbContext.CarPoolOpportunity.Where(x => x.OpportunityId == leaveOpportunity.OpportunityId).FirstOrDefaultAsync();

                getOpportunity.AvailableSeats = getOpportunity.AvailableSeats + 1;

                _dbContext.CarPoolOpportunity.Update(getOpportunity);
                await _dbContext.SaveChangesAsync();

                return "SUCCESS.Car Pool Opportunity Leave success!.";
            }
            else
                returnValue = "FAILED.Car Pool Opportunity capacity is 0!.";

            return returnValue;
        }

        public async Task<string> RegisterOpportunityAsync(RegisterOpportunityModel model)
        {
            string returnValue = "FAILED.Unable to Register Car Pool Opportunity";

            var carPool = new CarPoolOpportunity()
            {
                OpportunityName = model.OpportunityName,
                DepartureTime = model.DepartureTime,
                ArrivalTime = model.ArrivalTime,
                Origin = model.Origin,
                DaysAvailable = model.DaysAvailable,
                Destination = model.Destination,
                AvailableSeats = model.AvailableSeats,
                Notes = model.Notes,
                CreatedDate = DateTime.Now
            };

            //cannot register opportunity with overlapping time frames

            var overlappingOpportunity = await _dbContext.CarPoolOpportunity.ToListAsync();

            bool result = true;
            foreach(var o in overlappingOpportunity)
            {
                result = TimeFrameHelper.CheckTimeFrames(o.DepartureTime, o.ArrivalTime, model.DepartureTime, model.ArrivalTime);

                //if result is false, meaning there is opportunity with a time frame that overlaps exit the loop
                if (!result)
                    break;
            }

            //this if statement will allow user to register opportunity if there is no time overlapping
            if (!result)
                returnValue = $"FAILED.Cannot add an opportunity with overlapping time frames!";
            else
            {
                var register = await _dbContext.CarPoolOpportunity.AddAsync(carPool);// adds to car pool opportunity 
                await _dbContext.SaveChangesAsync();

                var createdOpportunityId = carPool.OpportunityId;
                if (createdOpportunityId > 0)
                {
                    //then addds to userOpportunity table to insert user record as a Lead on the Opportunity
                    var userOpportunity = new UserOpportunity()
                    {
                        UserId = model.UserId,
                        OpportunityId = createdOpportunityId,
                        IsLeader = true,//when a lead user creates opportunity ,this is set to true
                        JoinDate = DateTime.Now
                    };
                    await _dbContext.UserOpportunity.AddAsync(userOpportunity);
                    await _dbContext.SaveChangesAsync();

                    var createdUserOpportunityId = userOpportunity.Id;

                    if (createdUserOpportunityId > 0)
                        returnValue = $"SUCCESS.Car Pool Opportunity added successfully.";
                    else
                    {
                        var removeCarPool = await _dbContext.CarPoolOpportunity.Where(x => x.OpportunityId == createdOpportunityId).FirstOrDefaultAsync();
                        _dbContext.CarPoolOpportunity.Remove(removeCarPool);
                        await _dbContext.SaveChangesAsync();

                        returnValue = "FAILED.Failed to Complete Opportunity registration process assigned to the logged in user";
                    }
                }
                else
                    returnValue = "FAILED.Failed to register a car pool Opportunity";
            }

            return returnValue;
        }

        public async Task<IEnumerable<SearchOpportunityModel>> SearchOpportunitysAsync(SearchParameters search)
        {
            List<SearchOpportunityModel> searchOpportunities = new();
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                if (!string.IsNullOrEmpty(search.OpportunityName) && search.SearchType == "OpportunityName")
                {


                    var r = await cnn.QueryAsync<SearchOpportunityModel>($@"  Select  cpo.OpportunityId ,
			                                                                            UserId ,
			                                                                            UserName,
			                                                                            OpportunityName
			                                                                            DepartureTime,
			                                                                            ArrivalTime,
			                                                                            Origin,
			                                                                            DaysAvailable,
			                                                                            Destination,
			                                                                            AvailableSeats,
			                                                                            Notes,
			                                                                            IsLeader,
			                                                                            CreatedDate ,
			                                                                            JoinDate 
                                                                              FROM CoMute.dbo.CarPoolOpportunity cpo inner join 
                                                                                   CoMute.dbo.UserOpportunity up  on cpo.OpportunityId = up.OpportunityId inner join
	                                                                               CoMute.dbo.AspNetUsers u on u.Id = up.UserId
                                                                              where ltrim(rtrim(LOWER(cpo.OpportunityName)))  like ltrim(rtrim(LOWER('{search.OpportunityName}')))", commandTimeout: 0);

                    searchOpportunities = r.ToList();

                }
                if (!string.IsNullOrEmpty(search.Destination) && search.SearchType == "Destination")
                {
                    var r = await cnn.QueryAsync<SearchOpportunityModel>($@"  Select  cpo.OpportunityId ,
			                                                                            UserId ,
			                                                                            UserName,
			                                                                            OpportunityName
			                                                                            DepartureTime,
			                                                                            ArrivalTime,
			                                                                            Origin,
			                                                                            DaysAvailable,
			                                                                            Destination,
			                                                                            AvailableSeats,
			                                                                            Notes,
			                                                                            IsLeader,
			                                                                            CreatedDate ,
			                                                                            JoinDate 
                                                                              FROM CoMute.dbo.CarPoolOpportunity cpo inner join 
                                                                                   CoMute.dbo.UserOpportunity up  on cpo.OpportunityId = up.OpportunityId inner join
	                                                                               CoMute.dbo.AspNetUsers u on u.Id = up.UserId
                                                                              where ltrim(rtrim(LOWER(Destination)))  like ltrim(rtrim(LOWER('{search.Destination}')))", commandTimeout: 0);
                    searchOpportunities = r.ToList();
                }


                if (!string.IsNullOrEmpty(search.CreatedDate.ToString()) && search.SearchType == "CreatedDate")
                {
                    var r = await cnn.QueryAsync<SearchOpportunityModel>($@"  Select  cpo.OpportunityId ,
			                                                                            UserId ,
			                                                                            UserName,
			                                                                            OpportunityName
			                                                                            DepartureTime,
			                                                                            ArrivalTime,
			                                                                            Origin,
			                                                                            DaysAvailable,
			                                                                            Destination,
			                                                                            AvailableSeats,
			                                                                            Notes,
			                                                                            IsLeader,
			                                                                            CreatedDate ,
			                                                                            JoinDate 
                                                                              FROM CoMute.dbo.CarPoolOpportunity cpo inner join 
                                                                                   CoMute.dbo.UserOpportunity up  on cpo.OpportunityId = up.OpportunityId inner join
	                                                                               CoMute.dbo.AspNetUsers u on u.Id = up.UserId
                                                                              where Convert(date,cpo.CreatedDate) = Convert(date,'{search.CreatedDate.ToString("yyyy-MM-dd")}')", commandTimeout: 0);

                    searchOpportunities = r.ToList();
                }

                if (!string.IsNullOrEmpty(search.JoinDate.ToString()) && search.SearchType == "JoinDate")
                {
                    var r = await cnn.QueryAsync<SearchOpportunityModel>($@"  Select  cpo.OpportunityId ,
			                                                                            UserId ,
			                                                                            UserName,
			                                                                            OpportunityName
			                                                                            DepartureTime,
			                                                                            ArrivalTime,
			                                                                            Origin,
			                                                                            DaysAvailable,
			                                                                            Destination,
			                                                                            AvailableSeats,
			                                                                            Notes,
			                                                                            IsLeader,
			                                                                            CreatedDate ,
			                                                                            JoinDate 
                                                                              FROM CoMute.dbo.CarPoolOpportunity cpo inner join 
                                                                                   CoMute.dbo.UserOpportunity up  on cpo.OpportunityId = up.OpportunityId inner join
	                                                                               CoMute.dbo.AspNetUsers u on u.Id = up.UserId
                                                                              where Convert(date,up.JoinDate) = Convert(date,'{search.JoinDate.ToString("yyyy-MM-dd")}')", commandTimeout: 0);

                    searchOpportunities = r.ToList();
                }

                if(search.SearchType == "ALL" )
                {
                    var r = await cnn.QueryAsync<SearchOpportunityModel>($@"  Select  cpo.OpportunityId ,
			                                                                            UserId ,
			                                                                            UserName,
			                                                                            OpportunityName
			                                                                            DepartureTime,
			                                                                            ArrivalTime,
			                                                                            Origin,
			                                                                            DaysAvailable,
			                                                                            Destination,
			                                                                            AvailableSeats,
			                                                                            Notes,
			                                                                            IsLeader,
			                                                                            CreatedDate ,
			                                                                            JoinDate 
                                                                              FROM CoMute.dbo.CarPoolOpportunity cpo inner join 
                                                                                   CoMute.dbo.UserOpportunity up  on cpo.OpportunityId = up.OpportunityId inner join
	                                                                               CoMute.dbo.AspNetUsers u on u.Id = up.UserId", commandTimeout: 0);

                    searchOpportunities = r.ToList();
                }
            }
            return searchOpportunities;
        }
    }
}
