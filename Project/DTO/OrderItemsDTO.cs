namespace EcommerseApplication.DTO
{
    public class OrderItemsDTO
    {
        public int ItemID { get; set; }
        public int Quantity { get; set; }

        public int? ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
