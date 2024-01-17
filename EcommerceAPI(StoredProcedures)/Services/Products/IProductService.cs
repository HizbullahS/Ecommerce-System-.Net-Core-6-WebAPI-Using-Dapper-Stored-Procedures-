using CoreWebapi.Models;

namespace EcommerceAPI_StoredProcedures_.Services.Products
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
