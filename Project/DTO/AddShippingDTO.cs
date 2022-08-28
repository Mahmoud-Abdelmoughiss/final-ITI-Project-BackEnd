namespace EcommerseApplication.DTO
{
    public class AddShippingDTO
    {
        public string? shipName { get; set; }
        public string shippingstate { get; set; }//(Pick-up, on-process, on-delivery, Delivered)

        public string? arabicshippingstate { get; set; }
        public int shipperID { get; set; }
        public int? userID { get; set; }
    }
}
