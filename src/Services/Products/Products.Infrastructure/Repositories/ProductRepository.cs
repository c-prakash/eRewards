using eRewards.Services.Products.Domain.ProductsAggregate;
using eRewards.Services.Products.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace eRewards.Services.Products.Infrastructure.Repositories
{

    public class ProductRepository
        : IProductRepository
    {
        private readonly ProductDbContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public ProductRepository(ProductDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Product> Add(Product product)
        {
            var result = _context.Products.Add(product).Entity;
            await _context.SaveChangesAsync();

            return result;
        }

         

        public async Task<Product> GetAsync(int productId)
        {
            var action = await _context.Products.FirstOrDefaultAsync(o => o.Id == productId);
            
            if (action == null)
            {
                action = _context
                            .Products
                            .Local
                            .FirstOrDefault(o => o.Id == productId);
            }
            
            return action;

        }

        public async Task Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
