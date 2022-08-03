using AutoMapper;
using CoMute.Core.Domain;
using CoMute.Web.Models.Dto;

namespace CoMute.Web.App_Start.Bootstrappers.Ioc.AutoMapperProfiles
{
    public class UsersMappings : Profile
    {
        protected override void Configure()
        {
            CreateMap<User, RegistrationRequest>().ReverseMap();
        }
    }
}