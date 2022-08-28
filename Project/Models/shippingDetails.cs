using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerseApplication.Models
{
    public class shippingDetails
    {
        [Key]
        public int ID { get; set; }
        public string? shipName { get; set; }
        public string shippingstate { get; set; }//(Pick-up, on-process, on-delivery, Delivered)
        public string CustomerMobile { get; set; }

        public string? arabicshippingstate { get; set; }//(استلام شركة الشحن, جاري التنفيذ, جاري التوصيل, تم التوصيل)
        [ForeignKey("user")]
        public int userID { get; set; }  //relation with user
        [ForeignKey("shipper")]
        public int shipperID { get; set; } //relation with shiper

        [ForeignKey("order_Details")]
        public int orderDetailsID { get; set; }//relation with order detais
        [DataType(DataType.DateTime)]
        public DateTime? createdAt { get; set; }
        public DateTime? completeAt { get; set; }

        public DateTime? deletedAt { get; set; }
        public string? ALLaddress { get; set; }
        public string? ALLaddress_Ar { get; set; }

        [ForeignKey("user_Address")]
        public int? addressID { get; set; }//relation with Address
       public Shipper? shipper { get; set; }
      public  Order_Details? order_Details { get; set; }
       public User? user { get; set; }

        public User_address? user_Address { get; set; }
    }
}
