using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerseApplication.Models
{
    public class Order_Details
    {
        public int Id { get; set; }
        public int Total { get; set; }


        [ForeignKey("User")]
        public int? UserID { get; set; }
        [ForeignKey("Payment_Details")]
        public int? Payment_ID { get; set; }


        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }


        public virtual User? User { get; set; }
        public virtual Payment_Details? Payment_Details { get; set; }
        public virtual ICollection<Order_Items>? Order_Items { get; set; }
    }
}
