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
            try
            {
                var response = _httpClient.PostAsJsonAsync("api/products", product).Result;
                if (!response.IsSuccessStatusCode)
                {
                    // Handle the error response
                    throw new Exception($"Error adding product: {response.ReasonPhrase}");
                }
                return;

            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw;
            }
        }

        public void DeleteProduct(int id)
        {
            try
            {
                var response = _httpClient.DeleteAsync($"api/products/{id}").Result;
                if (!response.IsSuccessStatusCode)
                {
                    // Handle the error response
                    throw new Exception($"Error deleting product: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw;
            }
        }



        public Product GetProductById(int id)
        {
            try
            {
                var response = _httpClient.GetAsync($"api/products/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var product = response.Content.ReadFromJsonAsync<Product>().Result;
                    return product ?? new Product();
                }
                else
                {
                    // Handle the error response
                    throw new Exception($"Error fetching product: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw;
            }
        }
        public void UpdateProduct(Product product)
        {
            try
            {
                var response = _httpClient.PutAsJsonAsync($"api/products/{product.ProductId}", product).Result;
                if (!response.IsSuccessStatusCode)
                {
                    // Handle the error response
                    throw new Exception($"Error updating product: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw;
            }
        }
    }
}
