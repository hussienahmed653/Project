namespace Project.Domain
{
    public class OrderDetails
    {
        public int OrderID { get; set; }
        public Order Order { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public decimal UnitPrice { get; set; }
        public byte Quantity { get; set; }
        public float Discount { get; set; }
    }
}
