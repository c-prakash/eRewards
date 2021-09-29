using System.Threading.Tasks;

namespace AdminPortal.Web.Application.Interfaces.Repositories
{
    public interface IDocumentRepository
    {
        Task<bool> IsDocumentTypeUsed(int documentTypeId);

        Task<bool> IsDocumentExtendedAttributeUsed(int documentExtendedAttributeId);
    }
}