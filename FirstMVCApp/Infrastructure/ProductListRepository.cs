using FirstMVCApp.Models;

namespace FirstMVCApp.Infrastructure
{
    public class ProductListRepository : IProductRepository
    {
        static List<Product> products = new List<Product>
        {
            new Product { ProductId=1, ProductName = "Chai", UnitsInStock = 10, UnitPrice = 18.00m, Discontinued = false },
            new Product {ProductId = 2, ProductName = "Chang", UnitsInStock = 13, UnitPrice = 76.00m, Discontinued = false},
            new Product {ProductId = 3, ProductName = "Chatai", UnitsInStock = 11, UnitPrice = 33.00m, Discontinued = false},
            new Product {ProductId = 4, ProductName = "Chai Latte", UnitsInStock = 15, UnitPrice = 25.00m, Discontinued = false},
            new Product {ProductId = 5, ProductName = "Chai Tea", UnitsInStock = 20, UnitPrice = 20.00m, Discontinued = false}
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return products.ToList();
        }

        public Product GetProductById(int id)
        {
            return products.FirstOrDefault(p => p.ProductId == id)!;
        }

        public void AddProduct(Product product)
        {
            product.ProductId = products.Max(p => p.ProductId) + 1;
            products.Add(product);
        }

        public void DeleteProduct(int id)
        {
            var item = products.Find(p=> p.ProductId == id);
            if (item is not null)
                products.Remove(item); 
        }
       
        public void UpdateProduct(Product product)
        {
            var item = products.FirstOrDefault(c => c.ProductId == product.ProductId);
            if(item is not null)
            {
                products.Remove(item);
                products.Add(product);
            }
        }
    }
}
