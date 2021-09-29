using AutoMapper;
using AdminPortal.Web.Application.Features.Brands.Commands.AddEdit;
using AdminPortal.Web.Application.Features.Brands.Queries.GetAll;
using AdminPortal.Web.Application.Features.Brands.Queries.GetById;
using AdminPortal.Web.Domain.Entities.Catalog;

namespace AdminPortal.Web.Application.Mappings
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<AddEditBrandCommand, Brand>().ReverseMap();
            CreateMap<GetBrandByIdResponse, Brand>().ReverseMap();
            CreateMap<GetAllBrandsResponse, Brand>().ReverseMap();
        }
    }
}