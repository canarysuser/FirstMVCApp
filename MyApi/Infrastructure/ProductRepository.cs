
using Microsoft.EntityFrameworkCore;

namespace MyApi.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly NorthwindDbContext _db;
        private readonly ILogger<ProductRepository> _logger;
        
        public ProductRepository(
            NorthwindDbContext db,
            ILogger<ProductRepository> logger
            ) => (_logger, _db) = (logger, db);
        


        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _db.Products
                .AsNoTracking()
                .ToListAsync();
        }
        public Task<Product?> GetProductByIdAsync(int id)
        {
            return _db.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public Task<bool> UpsertProductAsync(Product product)
        {
            var existingProduct = _db.Products
                .AsNoTracking()
                .FirstOrDefault(p => p.ProductId == product.ProductId);

            if (existingProduct == null)
                {
                _db.Products.Add(product);
            }
            else
            {
                _db.Products.Update(product);
            }

            return _db.SaveChangesAsync()
                .ContinueWith(t => t.Result > 0);
        }

        public Task<bool> DeleteProductAsync(int id)
        {
            var product = _db.Products
                .AsNoTracking()
                .FirstOrDefault(p => p.ProductId == id);
            if (product != null)
            {
                _db.Products.Remove(product);
                return _db.SaveChangesAsync()
                    .ContinueWith(t => t.Result > 0);
            }
            return Task.FromResult(false);
        }

    }
}
