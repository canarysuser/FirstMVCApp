﻿using FirstMVCApp.Models;

namespace FirstMVCApp.Infrastructure
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts(); 
        Product GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        //IEnumerable<Product> SearchProducts(string searchTerm);
    }
}
