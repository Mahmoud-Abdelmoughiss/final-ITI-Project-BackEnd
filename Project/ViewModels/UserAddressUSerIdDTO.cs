using System.ComponentModel.DataAnnotations;

namespace EcommerseApplication.ViewModels
{
    public class UserAddressUSerIdDTO
    {
        [Required]
        public string AddressLine1 { get; set; }
        [Required]
        public string AddressLine2 { get; set; }
        [Required]
        [RegularExpression("[A-Za-z]{3,20}",ErrorMessage ="City name must more than 3 litter and less than 20 letter")]
        public string City { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        [RegularExpression("[A-Za-z]{3,20}")]
        public string Country { get; set; }
        [Required]
        public string telephone { get; set; }
        [Required]
        public string mobile { get; set; }
    }
}
