using CoreWebapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebapi.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProducts(int ProductId);
        Task<int> CreateProduct(Product product);
        Task<int> UpdateProduct(Product product);
        Task<int> DeleteProduct(int id);
    }
}
