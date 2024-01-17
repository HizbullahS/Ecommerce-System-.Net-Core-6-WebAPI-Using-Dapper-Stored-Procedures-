namespace EcommerceAPI_StoredProcedures_.Models
{
    public class TotalSales
    {
        public int SaleId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int TotalPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
