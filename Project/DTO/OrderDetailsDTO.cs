namespace EcommerseApplication.DTO
{
    public class OrderDetailsDTO
    {
        public int OrderId { get; set; }
        public int TotalPrice { get; set; }
        public DateTime? CreatedAt { get; set; }

        public List<OrderItemsDTO> OrderItems { get; set; }

    }
}
