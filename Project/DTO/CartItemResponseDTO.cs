namespace EcommerseApplication.DTO
{
    public class CartItemResponseDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDiscription { get; set; }
        public string ProductImage { get; set; }
        public int QuantityOrdered { get; set; }
        public int QuantityAvailable { get; set; }
    }
}
