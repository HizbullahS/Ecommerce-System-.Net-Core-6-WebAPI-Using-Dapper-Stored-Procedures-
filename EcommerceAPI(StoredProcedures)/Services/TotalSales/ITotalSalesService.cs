using EcommerceAPI_StoredProcedures_.Models;

public interface ITotalSalesService
{
    public Task<List<TotalSales>> GetAllSales();
    public Task<TotalSales> GetSaleById(int SaleId);
    public Task<int> CreateSale(TotalSales sales);
    public Task<int> UpdateSale(TotalSales sales);
    public Task<int> DeleteSale(int SaleId);
}

