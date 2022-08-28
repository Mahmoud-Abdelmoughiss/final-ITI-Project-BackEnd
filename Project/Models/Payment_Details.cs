namespace EcommerseApplication.Models
{
    public class Payment_Details
    {
        public int ID { get; set; }
        public int Amount { get; set; }
        public string Provider { get; set; }
        public string? Status { get; set; }
        public string? TransactionID { get; set; }


        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
