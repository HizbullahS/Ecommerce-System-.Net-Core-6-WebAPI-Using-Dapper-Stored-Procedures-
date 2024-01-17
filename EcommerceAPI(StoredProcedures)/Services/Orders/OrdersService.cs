using CoreWebapi.Services;
using Dapper;
using EcommerceAPI_StoredProcedures_.Models;
using System.Data;
using System.Data.SqlClient;

public class OrdersService : IOrdersService
{
    ///////////////////////////////////////////// GET /////////////////////////////////////
    public async Task<List<Orders>> GetAllOrders()
    {
        using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
        {
            var orders = await connection.QueryAsync<Orders>("SP_GetAllOrders", null, commandType: CommandType.StoredProcedure);
            return orders.ToList();
        }
    }

    ///////////////////////////////////////////// GET BY ID /////////////////////////////////////
    public async Task<Orders> GetOrderById(int OrderId)
    {
        using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OrderId", OrderId);
            var order = await connection.QueryAsync<Orders>("SP_GetOrderById", parameters, commandType: CommandType.StoredProcedure);
            return order.FirstOrDefault();
        }
    }

    ///////////////////////////////////////////// Create Order /////////////////////////////////////
    public async Task<int> CreateOrder(Orders orders)
    {
        using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ProductId", orders.ProductId);
            parameters.Add("@Status", orders.Status);
            parameters.Add("@ProductName", orders.ProductName);
            parameters.Add("@Quantity", orders.Quantity);
            parameters.Add("@TotalPrice", orders.TotalPrice);
            var order = await connection.ExecuteAsync("SP_Createorder", parameters, commandType: CommandType.StoredProcedure);
            return order;
        }
    }

    ///////////////////////////////////////////// Update Order /////////////////////////////////////
    public async Task<int> UpdateOrder(Orders orders)
    {
        using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OrderId", orders.OrderId);
            parameters.Add("@ProductId", orders.ProductId);
            parameters.Add("@Status", orders.Status);
            parameters.Add("@ProductName", orders.ProductName);
            parameters.Add("@TotalPrice", orders.TotalPrice);
            parameters.Add("@Quantity", orders.Quantity);

            var result = await connection.ExecuteAsync("SP_UpdateOrder", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }

    ///////////////////////////////////////////// Delete Order /////////////////////////////////////
    public async Task<int> DeleteOrder(int OrderId)
    {
        using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OrderId", OrderId);

            var result = await connection.ExecuteAsync("SP_DeleteOrder", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
