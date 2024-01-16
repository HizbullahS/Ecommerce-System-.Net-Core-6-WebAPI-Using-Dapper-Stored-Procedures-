using CoreWebapi.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace CoreWebapi.Services
{
    public class ProductService : IProductService
    {
        ///////////////////////////////////////////// GET //////////////////////////////////////////
        public async Task<List<Product>> GetProducts()
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                var products = await connection.QueryAsync<Product>("SP_GetAllProducts", null, commandType: CommandType.StoredProcedure);
                return products.ToList();
            }
        }

        ///////////////////////////////////////////// GET BY ID ////////////////////////////////////
        public async Task<Product> GetProducts(int ProductId)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ProductId", ProductId);
                var products = await connection.QueryAsync<Product>("SP_GetProductById", parameters, commandType: CommandType.StoredProcedure);
                return products.FirstOrDefault();
            }
        }

        ///////////////////////////////////////////// POST ////////////////////////////////////////
        public async Task<int> CreateProduct(Product product)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ProductName", product.ProductName);
                parameters.Add("@Description", product.Description);
                parameters.Add("@Manufacturer", product.Manufacturer);
                parameters.Add("@Category", product.Category);
                parameters.Add("@Price", product.Price);
                parameters.Add("@Quantity", product.Quantity);

                var result = await connection.ExecuteAsync("SP_CreateProduct", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        ///////////////////////////////////////////// UPDATE BY ID /////////////////////////////////
        public async Task<int> UpdateProduct(Product product)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ProductId", product.ProductId);
                parameters.Add("@ProductName", product.ProductName);
                parameters.Add("@Description", product.Description);
                parameters.Add("@Manufacturer", product.Manufacturer);
                parameters.Add("@Category", product.Category);
                parameters.Add("@Price", product.Price);
                parameters.Add("@Quantity", product.Quantity);

                var result = await connection.ExecuteAsync("SP_UpdateProduct", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        ///////////////////////////////////////////// DELETE /////////////////////////////////
        public async Task<int> DeleteProduct(int id)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ProductId", id);
                var result = await connection.ExecuteAsync("SP_DeleteProduct", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
