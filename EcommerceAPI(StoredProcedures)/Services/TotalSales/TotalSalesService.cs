using CoreWebapi.Services;
using Dapper;
using EcommerceAPI_StoredProcedures_.Models;
using System.Data;
using System.Data.SqlClient;

public class TotalSalesService : ITotalSalesService
{
    ///////////////////////////////////////////// GET /////////////////////////////////////
    public async Task<List<TotalSales>> GetAllSales()
    {
        using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
        {
            var sales = await connection.QueryAsync<TotalSales>("SP_GetAllSales", null, commandType: CommandType.StoredProcedure);
            return sales.ToList();
        }
    }

    ///////////////////////////////////////////// GET BY ID /////////////////////////////////////
    public async Task<TotalSales> GetSaleById(int SaleId)
    {
        using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@SaleId", SaleId);
            var order = await connection.QueryAsync<TotalSales>("SP_GetSaleById", parameters, commandType: CommandType.StoredProcedure);
            return order.FirstOrDefault();
        }
    }

    ///////////////////////////////////////////// Create Order /////////////////////////////////////
    public async Task<int> CreateSale(TotalSales sales)
    {
        using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OrderId", sales.OrderId);
            parameters.Add("@ProductId", sales.ProductId);
            parameters.Add("@ProductName", sales.ProductName);
            parameters.Add("@TotalPrice", sales.TotalPrice);
            parameters.Add("@Quantity", sales.Quantity);
            parameters.Add("@SaleDate", sales.SaleDate);
            var order = await connection.ExecuteAsync("SP_CreateSale", parameters, commandType: CommandType.StoredProcedure);
            return order;
        }
    }

    ///////////////////////////////////////////// Update Order /////////////////////////////////////
    public async Task<int> UpdateSale(TotalSales sales)
    {
        using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@SaleId", sales.SaleId);
            parameters.Add("@OrderId", sales.OrderId);
            parameters.Add("@ProductId", sales.ProductId);
            parameters.Add("@ProductName", sales.ProductName);
            parameters.Add("@TotalPrice", sales.TotalPrice);
            parameters.Add("@Quantity", sales.Quantity);
            parameters.Add("@SaleDate", sales.SaleDate);

            var result = await connection.ExecuteAsync("SP_UpdateSale", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }

    ///////////////////////////////////////////// Delete Order /////////////////////////////////////
    public async Task<int> DeleteSale(int SaleId)
    {
        using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@SaleId", SaleId);

            var result = await connection.ExecuteAsync("SP_DeleteSale", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
