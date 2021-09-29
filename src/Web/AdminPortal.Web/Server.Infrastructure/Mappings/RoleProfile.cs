using AutoMapper;
using AdminPortal.Web.Infrastructure.Models.Identity;
using AdminPortal.Web.Application.Responses.Identity;

namespace AdminPortal.Web.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, BlazorHeroRole>().ReverseMap();
        }
    }
}