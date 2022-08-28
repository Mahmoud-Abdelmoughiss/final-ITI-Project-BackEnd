namespace EcommerseApplication.Models
{
    public class Product_Inventory
    {
        public int ID { get; set; }
        public int Quantity { get; set; }


        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        
    }
}
