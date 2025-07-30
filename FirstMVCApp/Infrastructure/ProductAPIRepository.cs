using FirstMVCApp.Models;

namespace FirstMVCApp.Infrastructure
{
    public class ProductAPIRepository : IProductRepository
    {
        HttpClient _httpClient;
        public ProductAPIRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5273/");
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                var response = _httpClient.GetAsync("api/products").Result;
                if(response.IsSuccessStatusCode)
                {
                    var products = response.Content.ReadFromJsonAsync<IEnumerable<Product>>().Result;
                    return products ?? new List<Product>();
                }
                
            } catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw;
            }
            return new List<Product>();
        }



        public void AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        

        public Product GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
