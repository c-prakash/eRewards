using AutoMapper;
using AdminPortal.Web.Application.Features.Products.Commands.AddEdit;
using AdminPortal.Web.Domain.Entities.Catalog;

namespace AdminPortal.Web.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddEditProductCommand, Product>().ReverseMap();
        }
    }
}