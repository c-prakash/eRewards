using AdminPortal.Web.Application.Interfaces.Repositories;
using AdminPortal.Web.Domain.Entities.Catalog;

namespace AdminPortal.Web.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly IRepositoryAsync<Brand, int> _repository;

        public BrandRepository(IRepositoryAsync<Brand, int> repository)
        {
            _repository = repository;
        }
    }
}