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


        [HttpGet("authget")]
        public IActionResult GetProductsAfterAuth()
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if(token==null) 
                return Unauthorized("Token is missing or invalid.");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync("http://localhost:5001/api/auth/validate").Result;
            if(response.IsSuccessStatusCode)
            {
                var products = _productRepository.GetAllProductsAsync().Result;
                return Ok(products);
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Failed to retrieve products.");
            }
        }

    }
}
