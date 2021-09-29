using AutoMapper;
using AdminPortal.Web.Application.Features.DocumentTypes.Commands.AddEdit;
using AdminPortal.Web.Application.Features.DocumentTypes.Queries.GetAll;
using AdminPortal.Web.Application.Features.DocumentTypes.Queries.GetById;
using AdminPortal.Web.Domain.Entities.Misc;

namespace AdminPortal.Web.Application.Mappings
{
    public class DocumentTypeProfile : Profile
    {
        public DocumentTypeProfile()
        {
            CreateMap<AddEditDocumentTypeCommand, DocumentType>().ReverseMap();
            CreateMap<GetDocumentTypeByIdResponse, DocumentType>().ReverseMap();
            CreateMap<GetAllDocumentTypesResponse, DocumentType>().ReverseMap();
        }
    }
}