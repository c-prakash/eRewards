using AutoMapper;
using AdminPortal.Web.Application.Features.Documents.Commands.AddEdit;
using AdminPortal.Web.Application.Features.Documents.Queries.GetById;
using AdminPortal.Web.Domain.Entities.Misc;

namespace AdminPortal.Web.Application.Mappings
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<AddEditDocumentCommand, Document>().ReverseMap();
            CreateMap<GetDocumentByIdResponse, Document>().ReverseMap();
        }
    }
}