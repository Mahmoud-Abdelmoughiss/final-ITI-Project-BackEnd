namespace EcommerseApplication.DTO
{
    public class PaymentStatus
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public long? Amount { get; set; }
        public string? BalanceTransaction { get; set; }
    }
}
