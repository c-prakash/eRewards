using AutoMapper;
using AdminPortal.Web.Application.Requests.Identity;
using AdminPortal.Web.Application.Responses.Identity;

namespace AdminPortal.Web.Client.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<PermissionResponse, PermissionRequest>().ReverseMap();
            CreateMap<RoleClaimResponse, RoleClaimRequest>().ReverseMap();
        }
    }
}