using System.ComponentModel.DataAnnotations;

namespace EcommerseApplication.DTO
{
    public class UserPaymentResponseDTO
    {
        public int Id { get; set; }
        public string HolderName { get; set; }

        [RegularExpression("^[0-9]{16,16}$")]
        public string CardNumber { get; set; }

        [StringLength(maximumLength: 4, MinimumLength = 2)]
        public string ExpYear { get; set; }

        [MaxLength(2)]
        public string ExpMonth { get; set; }

        [RegularExpression("^[0-9]{3,3}$")]
        public string Cvc { get; set; }

        public string PayementType { get; set; }

        public string Provider { get; set; }

        public string? arabicProvider { get; set; }


    }
}
