using AutoMapper;
using AdminPortal.Web.Infrastructure.Models.Audit;
using AdminPortal.Web.Application.Responses.Audit;

namespace AdminPortal.Web.Infrastructure.Mappings
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditResponse, Audit>().ReverseMap();
        }
    }
}