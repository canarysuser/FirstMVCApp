namespace MyApi.Infrastructure
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product?> GetProductByIdAsync(int id);

        Task<bool> UpsertProductAsync(Product product);

        Task<bool> DeleteProductAsync(int id);
    }
}
