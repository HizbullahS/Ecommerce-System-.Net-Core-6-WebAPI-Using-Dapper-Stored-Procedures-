using EcommerceAPI_StoredProcedures_.Models;

public interface IOrdersService
{
    public Task<List<Orders>> GetAllOrders();
    public Task<Orders> GetOrderById(int OrderId);
    public Task<int> CreateOrder(Orders orders);
    public Task<int> UpdateOrder(Orders orders);
    public Task<int> DeleteOrder(int OrderId);
}

