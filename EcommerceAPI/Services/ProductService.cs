using CoreWebapi.Models;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using Unipluss.Sign.ExternalContract.Entities;


namespace CoreWebapi.Services
{
    public class ProductService : IProductService
    {
        ///////////////////////////////////////////// GET //////////////////////////////////////////
        public async Task<List<Product>> GetProducts()
        {
            var query = "SELECT * FROM Products;";

            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                var products = await connection.QueryAsync<Product>(query);
                return products.ToList();
            }
        }

        ///////////////////////////////////////////// GET BY ID/////////////////////////////////////
        public async Task<Product> GetProducts(int ProductId)
        {
            var query = $"SELECT * FROM Products where ProductId ={ProductId}";

            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                var products = await connection.QueryAsync<Product>(query);
                return products.FirstOrDefault();
            }
        }

        ///////////////////////////////////////////// POST ////////////////////////////////////////
        public async Task<int> CreateProduct(Product product)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                var query = $"insert into Products ( ProductName, Description, Manufacturer,Category,Price,Quantity ) values " +
                                  $"('{product.ProductName}','{product.Description}','{product.Manufacturer}','{product.Category}',{product.Price},{product.Quantity});";
                var result = await connection.ExecuteAsync(query);
                return result;
            }
        }

        ///////////////////////////////////////////// UPDATE BY ID /////////////////////////////////
        public async Task<int>UpdateProduct(Product product)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                var query = $"update Products set ProductName= '{product.ProductName}', Description= '{product.Description}', Manufacturer= '{product.Manufacturer}', Category ='{product.Category} ', Price = {product.Price}, Quantity= {product.Quantity}" +
                                  $"where ProductId ={product.ProductId}";
                var result = await connection.ExecuteAsync(query);
                return result;
            }
        }

        ///////////////////////////////////////////// DELETE /////////////////////////////////
        public async Task<int>DeleteProduct (int id)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                var query = $"delete from Products where ProductId ={id}";
                var result = await connection.ExecuteAsync(query);
                return result;
            }
        }

    }
}
