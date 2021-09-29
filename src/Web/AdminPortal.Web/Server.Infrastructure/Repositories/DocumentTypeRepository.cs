using AdminPortal.Web.Application.Interfaces.Repositories;
using AdminPortal.Web.Domain.Entities.Misc;

namespace AdminPortal.Web.Infrastructure.Repositories
{
    public class DocumentTypeRepository : IDocumentTypeRepository
    {
        private readonly IRepositoryAsync<DocumentType, int> _repository;

        public DocumentTypeRepository(IRepositoryAsync<DocumentType, int> repository)
        {
            _repository = repository;
        }
    }
}