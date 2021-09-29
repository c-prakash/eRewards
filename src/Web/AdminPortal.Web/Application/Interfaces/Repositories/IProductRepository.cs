using System.Threading.Tasks;

namespace AdminPortal.Web.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<bool> IsBrandUsed(int brandId);
    }
}