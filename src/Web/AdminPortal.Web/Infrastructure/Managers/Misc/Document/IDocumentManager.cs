using AdminPortal.Web.Application.Features.Documents.Commands.AddEdit;
using AdminPortal.Web.Application.Features.Documents.Queries.GetAll;
using AdminPortal.Web.Application.Requests.Documents;
using AdminPortal.Web.Shared.Wrapper;
using System.Threading.Tasks;
using AdminPortal.Web.Application.Features.Documents.Queries.GetById;

namespace AdminPortal.Web.Client.Infrastructure.Managers.Misc.Document
{
    public interface IDocumentManager : IManager
    {
        Task<PaginatedResult<GetAllDocumentsResponse>> GetAllAsync(GetAllPagedDocumentsRequest request);

        Task<IResult<GetDocumentByIdResponse>> GetByIdAsync(GetDocumentByIdQuery request);

        Task<IResult<int>> SaveAsync(AddEditDocumentCommand request);

        Task<IResult<int>> DeleteAsync(int id);
    }
}