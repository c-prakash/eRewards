using System.Collections.Generic;
using System.Threading.Tasks;
using AdminPortal.Web.Application.Features.DocumentTypes.Commands.AddEdit;
using AdminPortal.Web.Application.Features.DocumentTypes.Queries.GetAll;
using AdminPortal.Web.Shared.Wrapper;

namespace AdminPortal.Web.Client.Infrastructure.Managers.Misc.DocumentType
{
    public interface IDocumentTypeManager : IManager
    {
        Task<IResult<List<GetAllDocumentTypesResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditDocumentTypeCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}