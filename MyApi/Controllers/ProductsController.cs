using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApi.Infrastructure;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(
            IProductRepository productRepository,
            ILogger<ProductsController> logger
            )
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();
            if(products == null || !products.Any())
            {
                _logger.LogWarning("No products found.");
                return NotFound("No products available.");
            }
            return Ok(products);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> InsertProduct(Product product)
        {
            if (await _productRepository.UpsertProductAsync(product))
                return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
            return BadRequest("Failed to upsert product.");
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpsertProduct(int id, Product product)
        {
            if (await _productRepository.UpsertProductAsync(product))
                return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
            return BadRequest("Failed to upsert product.");
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (await _productRepository.DeleteProductAsync(id))
                return NoContent();
            return NotFound();
        }

    }
}
