using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerseApplication.Models
{
    public class User_Payement
    {
        [Key]
        public int Id { get; set; }
      
        public string PayementType { get; set; }
        public string? arabicPayementType { get; set; }
        public string Provider { get; set; }
        public string? arabicProvider { get; set; }
        //New
        public string HolderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpYear { get; set; }
        public string ExpMonth { get; set; }
        public string Cvc { get; set; }
        public string? StripePaymentToken { get; set; }
        /// 

        public int? AccountNo { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Expiry { get; set; }

        [ForeignKey("user")]
        public int UserId { get; set; }
       public virtual User user{ get; set; }
    }
}
