using AdminPortal.Web.Application.Features.Brands.Queries.GetAll;
using AdminPortal.Web.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdminPortal.Web.Application.Features.Brands.Commands.AddEdit;
using AdminPortal.Web.Application.Features.Brands.Commands.Import;

namespace AdminPortal.Web.Client.Infrastructure.Managers.Catalog.Brand
{
    public interface IBrandManager : IManager
    {
        Task<IResult<List<GetAllBrandsResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditBrandCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");

        Task<IResult<int>> ImportAsync(ImportBrandsCommand request);
    }
}