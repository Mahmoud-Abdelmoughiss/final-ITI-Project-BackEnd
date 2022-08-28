using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerseApplication.Models
{
    public class Shipper
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string? arabicName { get; set; }
        public string officePhone { get; set; }
        
        [ForeignKey("Identity")]
        public string? IdentityId { get; set; } //reference to AppUser and maps to it

        [DataType(DataType.DateTime)]
        public DateTime? deletedAt { get; set; }
        public List<shippingDetails>? shippingDetails { get; set; }
        public AppUser? Identity { get; set; }

    }
}
