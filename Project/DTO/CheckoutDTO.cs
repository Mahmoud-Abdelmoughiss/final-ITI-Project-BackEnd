using System.ComponentModel.DataAnnotations;

namespace EcommerseApplication.DTO
{
    public class CheckoutDTO
    {
        public int PaymentID { get; set; }
        
        public string? Currency { get; set; } = "USD";
        [Range(maximum:double.MaxValue,minimum: 1.0)]
        public decimal Amount { get; set; }

    }
}
