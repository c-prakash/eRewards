using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using eRewards.Services.Products.Domain.ProductsAggregate;

namespace eRewards.Services.Products.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductsController> _logger;

       /// <summary>
       /// 
       /// </summary>
       /// <param name="productRepository"></param>
       /// <param name="logger"></param>
        public ProductsController(
            IProductRepository productRepository,
            ILogger<ProductsController> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            var innerProduct = new Product
            {
            };

            var item = await _productRepository.Add(innerProduct);

            return CreatedAtAction(nameof(GetProduct), new { id = item.Id }, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Route("{productId:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Product>> GetProduct(int productId)
        {
            try
            {
                var product = await _productRepository.GetAsync(productId);

                return Ok(product);
            }
            catch
            {
                return NotFound();
            }
        }

    }
}
