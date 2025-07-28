using FirstMVCApp.Models;

namespace FirstMVCApp.Infrastructure
{
    public class ProductEFRepository : IProductRepository
    {
      private readonly NorthwindDbContext _context;
        private readonly ILogger<ProductEFRepository> _logger;
        public ProductEFRepository(
            NorthwindDbContext context,
            ILogger<ProductEFRepository> logger
            ) => (_context, _logger) = (context, logger);
      

        public IEnumerable<Product> GetAllProducts()
        {
            _logger.LogInformation(
                $"{nameof(ProductEFRepository)}.{nameof(GetAllProducts)}invoked");
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            //throw new NotImplementedException();
          //  _context.Products.Update(product);
            _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges(); //Executes the update statement

        }
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges(); //Executes the insert statement
        }

        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}
