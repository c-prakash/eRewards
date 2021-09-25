using eRewards.Services.Products.Domain.Seedwork;
using System.Threading.Tasks;

namespace eRewards.Services.Products.Domain.ProductsAggregate
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Order Aggregate

    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> Add(Product product);

        Task Update(Product product);


        Task<Product> GetAsync(int productId);
    }
}
