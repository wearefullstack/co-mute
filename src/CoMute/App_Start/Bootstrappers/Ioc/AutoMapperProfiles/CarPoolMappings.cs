using AutoMapper;
using CoMute.Core.Domain;
using CoMute.Web.Models.Dto;

namespace CoMute.Web.App_Start.Bootstrappers.Ioc.AutoMapperProfiles
{
    public class CarPoolMappings : Profile
    {
        protected override void Configure()
        {
            CreateMap<CarPoolOpportunity, CarPoolsOpportunityRequest>().ReverseMap();
        }
    }
}