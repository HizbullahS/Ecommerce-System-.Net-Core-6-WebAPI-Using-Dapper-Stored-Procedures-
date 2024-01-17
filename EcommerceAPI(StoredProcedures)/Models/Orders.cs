namespace EcommerceAPI_StoredProcedures_.Models
{
    public class Orders
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string Status { get; set; }
        public string ProductName { get; set; }
        public int TotalPrice { get; set; }
        public int Quantity { get; set; }

    }
}
