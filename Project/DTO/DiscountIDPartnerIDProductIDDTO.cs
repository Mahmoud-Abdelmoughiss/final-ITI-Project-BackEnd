using System.ComponentModel.DataAnnotations;

namespace EcommerseApplication.DTO
{
    public class DiscountIDPartnerIDProductIDDTO
    {
        [Required]
        public int DiscountId { get; set; }
        [Required]
        public int ProductID { get; set; }
    }
}
