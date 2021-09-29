using AdminPortal.Web.Application.Features.Products.Commands.AddEdit;
using AdminPortal.Web.Application.Features.Products.Queries.GetAllPaged;
using AdminPortal.Web.Application.Requests.Catalog;
using AdminPortal.Web.Shared.Wrapper;
using System.Threading.Tasks;

namespace AdminPortal.Web.Client.Infrastructure.Managers.Catalog.Product
{
    public interface IProductManager : IManager
    {
        Task<PaginatedResult<GetAllPagedProductsResponse>> GetProductsAsync(GetAllPagedProductsRequest request);

        Task<IResult<string>> GetProductImageAsync(int id);

        Task<IResult<int>> SaveAsync(AddEditProductCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}